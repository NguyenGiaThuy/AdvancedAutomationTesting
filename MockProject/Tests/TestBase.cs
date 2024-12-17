using OpenQA.Selenium.BiDi.Modules.BrowsingContext;

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
        var explicitTimeout = int.Parse(TestAssembly.Configuration["TestSettings:ExplicitTimeout"]!);
        var implicitTimeout = int.Parse(TestAssembly.Configuration["TestSettings:ImplicitTimeout"]!);
        var pageLoadTimeout = int.Parse(TestAssembly.Configuration["TestSettings:pageLoadTimeout"]!);

        _testContext = testContext;
        _testContext.WriteLine($"Class precondition runs in {_testContext.FullyQualifiedTestClassName}");

        try
        {
            new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            _webDriver = new FirefoxDriver();
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLoadTimeout);
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitTimeout);

            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(explicitTimeout));

            // Navigate to the site
            _webDriver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com");

            // Login with valid username and password
            _wait.Until(drv => drv.FindElement(By.XPath("//input[@name='username']"))).SendKeys("Admin");
            _wait.Until(drv => drv.FindElement(By.XPath("//input[@name='password']"))).SendKeys("admin123");
            _wait.Until(drv => drv.FindElement(
                    By.XPath("//button[contains(@class, 'orangehrm-login-button')]"))).Click();

            // Locate the profile picture to verify that the user is logged in
            _wait.Until(drv => drv.FindElement(By.XPath("//img[@alt='profile picture']")));
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
        _testContext.WriteLine($"Class postcondition runs in {_testContext.FullyQualifiedTestClassName}");
        _webDriver.Quit();
    }
}