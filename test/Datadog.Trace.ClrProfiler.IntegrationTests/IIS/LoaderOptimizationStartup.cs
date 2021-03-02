#if NETFRAMEWORK
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Datadog.Trace.ClrProfiler.IntegrationTests.IIS
{
    public class LoaderOptimizationStartup
    {
        private const string Url = "http://localhost:8080";

        public LoaderOptimizationStartup(ITestOutputHelper output)
        {
            Output = output;
        }

        private ITestOutputHelper Output { get; }

        [Fact]
        [Trait("RunOnWindows", "True")]
        [Trait("IIS", "True")]
        public async Task ApplicationDoesNotReturnErrors()
        {
            var intervalMilliseconds = 500;
            var intervals = 5;
            var serverReady = false;
            var client = new HttpClient();

            // wait for server to be ready to receive requests
            while (intervals-- > 0)
            {
                try
                {
                    var serverReadyResponse = await client.GetAsync(Url);
                    serverReady = serverReadyResponse.StatusCode == HttpStatusCode.OK;
                }
                catch
                {
                    // ignore
                }

                if (serverReady)
                {
                    Output.WriteLine("The server is ready.");
                    break;
                }

                Thread.Sleep(intervalMilliseconds);
            }

            // Server is ready to receive requests
            var responseMessage = await client.GetAsync(Url);
            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                Output.WriteLine($"Response status code: {responseMessage.StatusCode}\nResponse message content: {responseMessage.Content}");
            }

            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
        }
    }
}
#endif
