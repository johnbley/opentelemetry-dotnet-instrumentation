//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the LibraryVersionsGenerator tool. To safely
//     modify this file, edit PackageVersionDefinitions.cs and
//     re-run the LibraryVersionsGenerator project in Visual Studio.
// 
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated. 
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntegrationTests;

public static partial class LibraryVersion
{
    public static readonly IReadOnlyCollection<object[]> Azure = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "12.13.0" },
        new object[] { "12.19.1" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> Elasticsearch = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "8.0.0" },
        new object[] { "8.10.0" },
        new object[] { "8.13.3" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> EntityFrameworkCore = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "6.0.27" },
        new object[] { "7.0.16" },
#if NET8_0
        new object[] { "8.0.2" },
#endif
#if NET8_0
        new object[] { "8.0.3" },
#endif
#endif
    };
    public static readonly IReadOnlyCollection<object[]> EntityFrameworkCorePomeloMySql = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "6.0.2" },
        new object[] { "7.0.0" },
#if NET8_0
        new object[] { "8.0.0" },
#endif
#if NET8_0
        new object[] { "8.0.2" },
#endif
#endif
    };
    public static readonly IReadOnlyCollection<object[]> GraphQL = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "7.5.0" },
        new object[] { "7.8.0" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> GrpcNetClient = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "2.52.0" },
        new object[] { "2.62.0" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> MassTransit = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "8.0.0" },
        new object[] { "8.2.0" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> SqlClientMicrosoft = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "2.1.7" },
        new object[] { "3.1.5" },
        new object[] { "4.0.5" },
        new object[] { "5.2.0" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> SqlClientSystem = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "4.8.6" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> MongoDB = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "2.19.0" },
        new object[] { "2.24.0" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> MySqlConnector = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "2.0.0" },
        new object[] { "2.3.6" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> MySqlData = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "8.1.0" },
        new object[] { "8.3.0" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> Npgsql = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "6.0.0" },
        new object[] { "8.0.2" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> NServiceBus = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "8.0.0" },
        new object[] { "8.1.6" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> Quartz = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "3.4.0" },
        new object[] { "3.8.1" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> StackExchangeRedis = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "2.0.495" },
        new object[] { "2.1.50" },
        new object[] { "2.5.61" },
        new object[] { "2.6.66" },
        new object[] { "2.7.33" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> WCFCoreClient = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "4.10.2" },
        new object[] { "6.2.0" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> Kafka = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "1.6.2" },
        new object[] { "1.8.2" },
        new object[] { "2.3.0" },
#endif
    };
    public static readonly IReadOnlyCollection<object[]> Kafka_x64 = new List<object[]>
    {
#if DEFAULT_TEST_PACKAGE_VERSIONS
        new object[] { string.Empty }
#else
        new object[] { "1.4.0" },
#endif
    };
    public static readonly IReadOnlyDictionary<string, IReadOnlyCollection<object[]>> LookupMap = new Dictionary<string, IReadOnlyCollection<object[]>>
    {
       { "Azure", Azure },
       { "Elasticsearch", Elasticsearch },
       { "EntityFrameworkCore", EntityFrameworkCore },
       { "EntityFrameworkCorePomeloMySql", EntityFrameworkCorePomeloMySql },
       { "GraphQL", GraphQL },
       { "GrpcNetClient", GrpcNetClient },
       { "MassTransit", MassTransit },
       { "SqlClientMicrosoft", SqlClientMicrosoft },
       { "SqlClientSystem", SqlClientSystem },
       { "MongoDB", MongoDB },
       { "MySqlConnector", MySqlConnector },
       { "MySqlData", MySqlData },
       { "Npgsql", Npgsql },
       { "NServiceBus", NServiceBus },
       { "Quartz", Quartz },
       { "StackExchangeRedis", StackExchangeRedis },
       { "WCFCoreClient", WCFCoreClient },
       { "Kafka", Kafka },
       { "Kafka_x64", Kafka_x64 },
    };
}
