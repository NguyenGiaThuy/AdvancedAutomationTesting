namespace MockProject.Tests;

[TestClass]
public class TestBase : IDisposable
{
    protected static TestContext _testContext = null!;
    protected static IConfiguration _browserConfiguration = null!;
    protected IBrowser _browser = null!;
    protected string _baseUrl = null!;

    [AssemblyInitialize]
    public static void AssemblyPrecondition(TestContext testContext)
    {
        _testContext = testContext;

        var environment = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "Development";
        _browserConfiguration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .Build();
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
            var launchBrowser =
                Environment.GetEnvironmentVariable("BROWSER") ?? _browserConfiguration["Browser"]!;
            var implicitTimeout =
                Environment.GetEnvironmentVariable("IMPLICIT_TIMEOUT") != null
                    ? int.Parse(Environment.GetEnvironmentVariable("IMPLICIT_TIMEOUT")!)
                    : int.Parse(_browserConfiguration["ImplicitTimeout"]!);
            var browserOptions = _browserConfiguration["BrowserOptions"];
            _browser = BrowserFactory.MakeBrowser(launchBrowser, implicitTimeout, browserOptions);

            // Configure the base URL, username and password
            _baseUrl = _browserConfiguration["BaseUrl"]!;
            var username = Environment.GetEnvironmentVariable("TEST_USERNAME")!;
            var password = Environment.GetEnvironmentVariable("TEST_PASSWORD")!;

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
