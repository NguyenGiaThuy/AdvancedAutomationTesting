namespace MockProject.Tests;

[TestClass]
public class TestBase
{
    protected static TestContext _testContext = default!;
    protected static IWebDriver _webDriver = default!;
    protected static WebDriverWait _wait = default!;

    [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void ClassPrecondition(TestContext testContext)
    {
        var explicitTimeout = int.Parse(
            TestAssembly.Configuration["TestSettings:ExplicitTimeout"]!
        );
        var implicitTimeout = int.Parse(
            TestAssembly.Configuration["TestSettings:ImplicitTimeout"]!
        );

        _testContext = testContext;
        _testContext.WriteLine(
            $"Class precondition runs in {_testContext.FullyQualifiedTestClassName}"
        );

        try
        {
            new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            _webDriver = new FirefoxDriver();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(
                implicitTimeout
            );
            _wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(explicitTimeout));

            // Navigate to the site
            var loginPage = new LoginPage(
                _webDriver,
                _wait,
                "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login"
            );

            // Login with valid username and password
            loginPage.EnterUsername("Admin");
            loginPage.EnterPassword("admin123");
            loginPage.ClickLoginButton();

            // Locate the profile picture to verify that the user is logged in
            _wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                    By.XPath("//img[@alt='profile picture']")
                )
            );
        }
        catch (WebException ex)
        {
            throw new PreconditionException("Failed to load page due to connection", ex);
        }
        catch (TimeoutException ex)
        {
            throw new PreconditionException("Failed to load page due to timeout", ex);
        }
        catch (NoSuchElementException ex)
        {
            throw new PreconditionException("Failed to login", ex);
        }
    }

    [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass, ClassCleanupBehavior.EndOfClass)]
    public static void ClassPostcondition()
    {
        _testContext.WriteLine(
            $"Class postcondition runs in {_testContext.FullyQualifiedTestClassName}"
        );
        _webDriver.Quit();
    }
}
