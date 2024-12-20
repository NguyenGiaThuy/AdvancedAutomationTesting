namespace MockProject.Tests;

[TestClass]
public class TestBase : IDisposable
{
    protected static TestContext _testContext = null!;
    protected static IConfiguration _configuration = null!;
    protected IBrowser _browser = null!;

    [AssemblyInitialize]
    public static void AssemblyPrecondition(TestContext testContext)
    {
        _testContext = testContext;
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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

            _browser = Browser.BuildBrowser(_configuration);

            // Navigate to the site
            var loginPage = new LoginPage(_browser);
            loginPage.GoToPage();

            // Login with valid username and password
            loginPage.LoginUser("Admin", "admin123");

            // Locate the profile picture to verify that the user is logged in
            var homePage = new HomePage(_browser);
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
