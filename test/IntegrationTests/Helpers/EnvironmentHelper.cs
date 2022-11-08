// <copyright file="EnvironmentHelper.cs" company="OpenTelemetry Authors">
// Copyright The OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Xunit.Abstractions;

namespace IntegrationTests.Helpers;

public class EnvironmentHelper
{
    private static readonly Assembly ExecutingAssembly = Assembly.GetExecutingAssembly();
    private static readonly string RuntimeFrameworkDescription = RuntimeInformation.FrameworkDescription.ToLower();
    private static string _nukeOutputLocation;

    private readonly ITestOutputHelper _output;
    private readonly int _major;
    private readonly int _minor;
    private readonly string _patch = null;

    private readonly string _appNamePrepend;
    private readonly string _runtime;
    private readonly bool _isCoreClr;
    private readonly string _testApplicationDirectory;
    private readonly TargetFrameworkAttribute _targetFramework;

    private string _integrationsFileLocation;
    private string _profilerFileLocation;

    public EnvironmentHelper(
        string testApplicationName,
        Type anchorType,
        ITestOutputHelper output,
        string testApplicationDirectory = null,
        bool prependTestApplicationToAppName = true)
    {
        TestApplicationName = testApplicationName;
        _testApplicationDirectory = testApplicationDirectory ?? Path.Combine("test", "test-applications", "integrations");
        _targetFramework = Assembly.GetAssembly(anchorType).GetCustomAttribute<TargetFrameworkAttribute>();
        _output = output;

        var parts = _targetFramework.FrameworkName.Split(',');
        _runtime = parts[0];
        _isCoreClr = _runtime.Equals(EnvironmentTools.CoreFramework);

        var versionParts = parts[1].Replace("Version=v", string.Empty).Split('.');
        _major = int.Parse(versionParts[0]);
        _minor = int.Parse(versionParts[1]);

        if (versionParts.Length == 3)
        {
            _patch = versionParts[2];
        }

        _appNamePrepend = prependTestApplicationToAppName
            ? "TestApplication."
            : string.Empty;

        SetDefaultEnvironmentVariables();
    }

    public bool DebugModeEnabled { get; set; } = true;

    public Dictionary<string, string> CustomEnvironmentVariables { get; set; } = new Dictionary<string, string>();

    public string TestApplicationName { get; }

    public string FullTestApplicationName => $"{_appNamePrepend}{TestApplicationName}";

    public static bool IsCoreClr()
    {
        return RuntimeFrameworkDescription.Contains("core") || Environment.Version.Major >= 5;
    }

    public static string GetNukeBuildOutput()
    {
        string nukeOutputPath = Path.Combine(
            EnvironmentTools.GetSolutionDirectory(),
            "bin",
            "tracer-home");

        if (Directory.Exists(nukeOutputPath))
        {
            _nukeOutputLocation = nukeOutputPath;

            return _nukeOutputLocation;
        }

        throw new Exception($"Unable to find Nuke output at: {nukeOutputPath}. Ensure Nuke has run first.");
    }

    public static bool IsRunningOnCI()
    {
        // https://docs.github.com/en/actions/learn-github-actions/environment-variables#default-environment-variables
        // Github sets CI environment variable

        string env = Environment.GetEnvironmentVariable("CI");
        return !string.IsNullOrEmpty(env);
    }

    public void SetEnvironmentVariables(StringDictionary environmentVariables, string processToProfile)
    {
        if (!string.IsNullOrEmpty(processToProfile))
        {
            environmentVariables["OTEL_DOTNET_AUTO_INCLUDE_PROCESSES"] = Path.GetFileName(processToProfile);
        }

        foreach (var key in CustomEnvironmentVariables.Keys)
        {
            environmentVariables[key] = CustomEnvironmentVariables[key];
        }
    }

    public string GetProfilerPath()
    {
        if (_profilerFileLocation != null)
        {
            return _profilerFileLocation;
        }

        string extension = EnvironmentTools.GetOS() switch
        {
            "win" => "dll",
            "linux" => "so",
            "osx" => "dylib",
            _ => throw new PlatformNotSupportedException()
        };

        string fileName = $"OpenTelemetry.AutoInstrumentation.Native.{extension}";
        string nukeOutput = GetNukeBuildOutput();
        string profilerPath = EnvironmentTools.IsWindows()
            ? Path.Combine(nukeOutput, $"win-{EnvironmentTools.GetPlatform().ToLower()}", fileName)
            : Path.Combine(nukeOutput, fileName);

        if (File.Exists(profilerPath))
        {
            _profilerFileLocation = profilerPath;
            _output?.WriteLine($"Found profiler at {_profilerFileLocation}.");
            return _profilerFileLocation;
        }

        throw new Exception($"Unable to find profiler at: {profilerPath}");
    }

    public string GetIntegrationsPath()
    {
        if (_integrationsFileLocation != null)
        {
            return _integrationsFileLocation;
        }

        string fileName = $"integrations.json";
        string integrationsPath = Path.Combine(GetNukeBuildOutput(), fileName);

        if (File.Exists(integrationsPath))
        {
            _integrationsFileLocation = integrationsPath;
            _output?.WriteLine($"Found integrations at {_profilerFileLocation}.");
            return _integrationsFileLocation;
        }

        throw new Exception($"Unable to find integrations at: {integrationsPath}");
    }

    public string GetTestApplicationPath(string packageVersion = "", string framework = "")
    {
        string extension = "exe";

        if (IsCoreClr() || _testApplicationDirectory.Contains("aspnet"))
        {
            extension = "dll";
        }

        var appFileName = $"{FullTestApplicationName}.{extension}";
        var testApplicationPath = Path.Combine(GetTestApplicationApplicationOutputDirectory(packageVersion: packageVersion, framework: framework), appFileName);
        return testApplicationPath;
    }

    public string GetTestApplicationExecutionSource()
    {
        string executor;

        if (_testApplicationDirectory.Contains("aspnet"))
        {
            executor = $"C:\\Program Files{(Environment.Is64BitProcess ? string.Empty : " (x86)")}\\IIS Express\\iisexpress.exe";
        }
        else if (IsCoreClr())
        {
            executor = EnvironmentTools.IsWindows() ? "dotnet.exe" : "dotnet";
        }
        else
        {
            var appFileName = $"{FullTestApplicationName}.exe";
            executor = Path.Combine(GetTestApplicationApplicationOutputDirectory(), appFileName);

            if (!File.Exists(executor))
            {
                throw new Exception($"Unable to find executing assembly at {executor}");
            }
        }

        return executor;
    }

    public string GetTestApplicationProjectDirectory()
    {
        var solutionDirectory = EnvironmentTools.GetSolutionDirectory();
        var projectDir = Path.Combine(
            solutionDirectory,
            _testApplicationDirectory,
            $"{FullTestApplicationName}");
        return projectDir;
    }

    public string GetTestApplicationApplicationOutputDirectory(string packageVersion = "", string framework = "")
    {
        var targetFramework = string.IsNullOrEmpty(framework) ? GetTargetFramework() : framework;
        var binDir = Path.Combine(
            GetTestApplicationProjectDirectory(),
            "bin");

        if (_testApplicationDirectory.Contains("aspnet"))
        {
            return Path.Combine(
                binDir,
                EnvironmentTools.GetBuildConfiguration(),
                "app.publish");
        }

        return Path.Combine(
            binDir,
            packageVersion,
            EnvironmentTools.GetPlatform().ToLowerInvariant(),
            EnvironmentTools.GetBuildConfiguration(),
            targetFramework);
    }

    public string GetTargetFramework()
    {
        if (_isCoreClr)
        {
            if (_major >= 5)
            {
                return $"net{_major}.{_minor}";
            }

            return $"netcoreapp{_major}.{_minor}";
        }

        return $"net{_major}{_minor}{_patch ?? string.Empty}";
    }

    private static string GetStartupHookOutputPath()
    {
        string startupHookOutputPath = Path.Combine(
            GetNukeBuildOutput(),
            "netcoreapp3.1",
            "OpenTelemetry.AutoInstrumentation.StartupHook.dll");

        return startupHookOutputPath;
    }

    private static string GetSharedStorePath()
    {
        string storePath = Path.Combine(
            GetNukeBuildOutput(),
            "store");

        return storePath;
    }

    private static string GetAdditionalDepsPath()
    {
        string additionalDeps = Path.Combine(
            GetNukeBuildOutput(),
            "AdditionalDeps");

        return additionalDeps;
    }

    private void SetDefaultEnvironmentVariables()
    {
        string profilerPath = GetProfilerPath();

        CustomEnvironmentVariables["DOTNET_STARTUP_HOOKS"] = GetStartupHookOutputPath();
        CustomEnvironmentVariables["DOTNET_SHARED_STORE"] = GetSharedStorePath();
        CustomEnvironmentVariables["DOTNET_ADDITIONAL_DEPS"] = GetAdditionalDepsPath();

        // when bytecode instrumentation is needed
        // CoreCLR Profiler must be expliclitly enabled in test in needed by setting CORECLR_ENABLE_PROFILING=1
        CustomEnvironmentVariables["CORECLR_PROFILER"] = EnvironmentTools.ProfilerClsId;
        CustomEnvironmentVariables["CORECLR_PROFILER_PATH"] = profilerPath;

        CustomEnvironmentVariables["COR_ENABLE_PROFILING"] = "1";
        CustomEnvironmentVariables["COR_PROFILER"] = EnvironmentTools.ProfilerClsId;
        CustomEnvironmentVariables["COR_PROFILER_PATH"] = profilerPath;

        CustomEnvironmentVariables["OTEL_DOTNET_AUTO_DEBUG"] = "1";
        CustomEnvironmentVariables["OTEL_DOTNET_AUTO_LOG_DIRECTORY"] = Path.Combine(EnvironmentTools.GetSolutionDirectory(), "build_data", "profiler-logs");
        CustomEnvironmentVariables["OTEL_DOTNET_AUTO_HOME"] = GetNukeBuildOutput();
        CustomEnvironmentVariables["OTEL_DOTNET_AUTO_INTEGRATIONS_FILE"] = Environment.GetEnvironmentVariable("OTEL_DOTNET_AUTO_INTEGRATIONS_FILE") ?? GetIntegrationsPath();
        CustomEnvironmentVariables["OTEL_DOTNET_AUTO_TRACES_ADDITIONAL_SOURCES"] = "TestApplication.*";
    }
}
