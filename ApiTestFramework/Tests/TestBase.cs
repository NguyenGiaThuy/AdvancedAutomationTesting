namespace ApiTestFramework.Tests;

[TestClass]
public class TestBase
{
    protected static TestContext _testContext = default!;
    protected static ApiConfiguration _apiConfiguration = default!;
    protected static ApiEnvironmentHelper _environmentHelper = default!;
    protected static ReportHelper _reportHelper = default!;
    protected IApiClient _client = default!;
    protected string _baseUrl = default!;

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

    [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void ClassPrecondition(TestContext testContext)
    {
        try
        {
            // Configure report
            _testContext.WriteLine("Setting up report...");
            _reportHelper = new ReportHelper();
            _reportHelper.InitializeReport(
                _apiConfiguration.GetReportDir()!,
                _apiConfiguration.GetReportFile()!
            );

            _testContext.WriteLine("Successfully set up report");
        }
        catch (Exception ex)
        {
            _testContext.WriteLine("Failed to set up report");
            throw new PreconditionException("Failed to set up report", ex);
        }
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

            // Create test case info for report
            var testMethodName = _testContext.TestName;
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
            _reportHelper.CreateTestCase(testMethodName!, testMethodDescription!);

            // Configure the client
            _testContext.WriteLine("Setting up REST client...");

            _baseUrl = _apiConfiguration.GetBaseUrl()!;
            _client = ApiFactory.MakeApiClient(_baseUrl);

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
        if (_testContext.CurrentTestOutcome == UnitTestOutcome.Failed)
        {
            _reportHelper.LogMessage(Status.Fail, "Test case failed");
        }
        else
        {
            _reportHelper.LogMessage(Status.Pass, "Test case passed");
        }

        _testContext.WriteLine("Closing REST client...");
        _client.Dispose();
    }

    [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass, ClassCleanupBehavior.EndOfClass)]
    public static void ClassPostcondition()
    {
        _reportHelper.ExportReport();
    }
}
