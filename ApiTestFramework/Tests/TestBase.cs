namespace ApiTestFramework.Tests;

[TestClass]
public class TestBase
{
    public TestContext TestContext { get; set; } = default!;
    protected static ApiConfiguration _apiConfiguration = default!;
    protected static ApiEnvironmentHelper _environmentHelper = default!;
    protected IApiClient _client = default!;
    protected string _baseUrl = default!;
    protected ExtentTest _test = default!;

    [AssemblyInitialize]
    public static void AssemblyPrecondition(TestContext testContext)
    {
        // _testContext = testContext;

        // Configure the browser configuration
        _environmentHelper = new ApiEnvironmentHelper();
        var environment = _environmentHelper.GetTestEnvironment() ?? "Development";
        _apiConfiguration = new ApiConfiguration(
            "appsettings.json",
            $"appsettings.{environment}.json"
        );

        // Configure report
        ReportHelper.InitializeReport(
            _apiConfiguration.GetReportDir()!,
            _apiConfiguration.GetReportFile()!
        );
    }

    [TestInitialize]
    public virtual void TestPrecondition()
    {
        try
        {
            TestContext.WriteLine("Current test suite: " + TestContext.FullyQualifiedTestClassName);
            TestContext.WriteLine("Current test case: " + TestContext.TestName);

            // Create test case info for report
            var testMethodName = TestContext.TestName;
            var method = GetType()
                .GetMethods()
                .FirstOrDefault(method =>
                    method.GetCustomAttributes(typeof(TestMethodAttribute), false).Any()
                    && method.Name == testMethodName
                );
            var testMethodAttribute = method
                ?.GetCustomAttributes(typeof(TestMethodAttribute), false)
                .Cast<TestMethodAttribute>()
                .FirstOrDefault();
            var testMethodDescription = testMethodAttribute?.DisplayName ?? testMethodName;
            _test = ReportHelper.CreateTest(testMethodName!, testMethodDescription!);

            // Configure the client
            TestContext.WriteLine("Setting up REST client...");

            _baseUrl = _apiConfiguration.GetBaseUrl()!;
            _client = ApiFactory.MakeApiClient(_baseUrl);

            TestContext.WriteLine("Successfully set up REST client");
        }
        catch (Exception ex)
        {
            TestContext.WriteLine("Failed to set up REST client");
            throw new PreconditionException("Failed to set up REST client", ex);
        }
    }

    [TestCleanup]
    public virtual void TestPostcondition()
    {
        var status =
            TestContext.CurrentTestOutcome == UnitTestOutcome.Failed ? Status.Fail : Status.Pass;
        ReportHelper.LogMessage(
            _test,
            status,
            $"Test case {TestContext.TestName} {status.ToString().ToLower()}."
        );

        TestContext.WriteLine("Closing REST client...");
        _client.Dispose();
    }

    [AssemblyCleanup]
    public static void AssemblyPostcondition()
    {
        ReportHelper.GenerateReport();
    }
}
