namespace SeleniumTestFramework.Tests;

[TestClass]
public class TestBase
{
    public TestContext TestContext { get; set; } = default!;
    protected static BrowserConfiguration _browserConfiguration = default!;
    protected static BrowserEnvironmentHelper _environmentHelper = default!;
    protected IBrowser _browser = default!;
    protected string _baseUrl = default!;

    [AssemblyInitialize]
    public static void AssemblyPrecondition(TestContext testContext)
    {
        // Configure the browser configuration
        _environmentHelper = new BrowserEnvironmentHelper();
        var environment = _environmentHelper.GetTestEnvironment() ?? "Development";
        _browserConfiguration = new BrowserConfiguration(
            "appsettings.json",
            $"appsettings.{environment}.json"
        );
    }

    [TestInitialize]
    public virtual void TestPrecondition()
    {
        try
        {
            TestContext.WriteLine("Current test suite: " + TestContext.FullyQualifiedTestClassName);
            TestContext.WriteLine("Current test case: " + TestContext.TestName);

            // Configure the browser
            TestContext.WriteLine("Logging in...");

            var browserType =
                _environmentHelper.GetBrowserType()
                ?? _browserConfiguration.GetBrowserType()
                ?? BrowserType.Firefox;
            var implicitTimeout =
                _environmentHelper.GetImplicitTimeout()
                ?? _browserConfiguration.GetImplicitTimeout()
                ?? 0;
            var browserOptions = _browserConfiguration.GetBrowserOptions();
            _browser = BrowserFactory.MakeBrowser(browserType, implicitTimeout, browserOptions);

            // Configure the page base URL, username and password
            _baseUrl = _browserConfiguration.GetBaseUrl()!;
            var username = _environmentHelper.GetVariable("TEST_USERNAME")!;
            var password = _environmentHelper.GetVariable("TEST_PASSWORD")!;

            // Login with valid username and password
            var loginPage = new LoginPage(_browser, _baseUrl);
            loginPage.GoToPage();
            loginPage.LoginUser(username, password);

            // Locate the profile picture to verify that the user is logged in
            var homePage = new HomePage(_browser, _baseUrl);
            homePage.GetProfilePicture();
            TestContext.WriteLine("Successfully logged in");
        }
        catch (WebException ex)
        {
            TestContext.WriteLine($"Failed to load page due to connection\nEx: {ex}");
            throw new PreconditionException("Failed to load page due to connection", ex);
        }
        catch (NoSuchElementException ex)
        {
            TestContext.WriteLine($"Failed to login\nEx: {ex}");
            throw new PreconditionException("Failed to login", ex);
        }
    }

    [TestCleanup]
    public void TestPostcondition()
    {
        TestContext.WriteLine($"Closing browser...");
        _browser.Quit();
    }
}
