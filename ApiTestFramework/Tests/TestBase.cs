namespace ApiTestFramework.Tests;

[TestClass]
public class TestBase
{
    protected static TestContext _testContext = null!;
    protected static ApiConfiguration _apiConfiguration = null!;
    protected static ApiEnvironmentHelper _environmentHelper = null!;
    protected IApiClient _client = null!;
    protected string _baseUrl = null!;

    [AssemblyInitialize]
    public static void AssemblyPrecondition(TestContext testContext)
    {
        _testContext = testContext;

        // Configure the browser configuration
        _environmentHelper = new ApiEnvironmentHelper();
        var environment = _environmentHelper.GetTestEnvironment() ?? "Development";
        _apiConfiguration = new ApiConfiguration(
            "appsettings.json",
            $"appsettings.{environment}.json"
        );
    }

    [TestInitialize]
    public virtual void TestPrecondition()
    {
        try
        {
            _testContext.WriteLine(
                "Current test suite: " + _testContext.FullyQualifiedTestClassName
            );
            _testContext.WriteLine("Current test case: " + _testContext.TestName);

            // Configure the client
            _baseUrl = _apiConfiguration.GetBaseUrl()!;
            _client = ApiFactory.MakeApi(_baseUrl);

            _testContext.WriteLine("Successfully set up REST client");
        }
        catch (Exception ex)
        {
            _testContext.WriteLine("Failed to set up REST client");
            throw new PreconditionException("Failed to set up REST client", ex);
        }
    }

    [TestCleanup]
    public virtual void TestPostcondition()
    {
        _testContext.WriteLine("Closing REST client...");
        _client.Dispose();
    }
}
