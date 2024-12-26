namespace MockProject.Tests;

[TestClass]
public class TestBase : IDisposable
{
    protected static TestContext _testContext = null!;
    protected static BrowserConfiguration _browserConfiguration = null!;
    protected static BrowserEnvironmentHelper _environmentHelper = null!;
    protected IBrowser _browser = null!;
    protected string _baseUrl = null!;

    [AssemblyInitialize]
    public static void AssemblyPrecondition(TestContext testContext)
    {
        _testContext = testContext;

        // Configure the browser configuration
        _environmentHelper = new BrowserEnvironmentHelper();
        var environment = _environmentHelper.GetTestEnvironment() ?? "Development";
        _browserConfiguration = new BrowserConfiguration(
            "appsettings.json",
            $"appsettings.{environment}.json"
        );
    }

    public TestBase()
    {
        try
        {
            _testContext.WriteLine(
                "Current test suite: " + _testContext.FullyQualifiedTestClassName
            );
            _testContext.WriteLine("Current test case: " + _testContext.TestName);

            // Configure the browser
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

            // Configure the base URL, username and password
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
            _testContext.WriteLine("Successfully logged in");
        }
        catch (WebException ex)
        {
            _testContext.WriteLine("Failed to load page due to connection");
            throw new PreconditionException("Failed to load page due to connection", ex);
        }
        catch (NoSuchElementException ex)
        {
            _testContext.WriteLine("Failed to login");
            throw new PreconditionException("Failed to login", ex);
        }
    }

    public void Dispose()
    {
        _testContext.WriteLine("Closing browser");
        _browser.Quit();
    }
}
