namespace MockProject.Browsers;

public class Browser : IBrowser
{
    private IWebDriver _webDriver = null!;

    /// <summary>
    /// Build the browser based on the configuration
    /// </summary>
    /// <param name="configuration">The configuration</param>
    /// <returns>The browser</returns>
    public static Browser BuildBrowser(IConfiguration configuration)
    {
        IDriverConfig driverConfig;
        IWebDriver webDriver;
        switch (configuration["TestSettings:Browser"])
        {
            case "Firefox":
                driverConfig = new FirefoxConfig();
                webDriver = new FirefoxDriver();
                break;
            case "Chrome":
                driverConfig = new ChromeConfig();
                webDriver = new ChromeDriver();
                break;
            case "Edge":
                driverConfig = new EdgeConfig();
                webDriver = new EdgeDriver();
                break;
            default:
                throw new WebDriverException("Invalid browser configuration");
        }

        var browser = new Browser();
        new WebDriverManager.DriverManager().SetUpDriver(driverConfig);
        browser._webDriver = webDriver;
        browser._webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(
            int.Parse(configuration["TestSettings:ImplicitTimeout"]!)
        );

        return browser;
    }

    public void GoToUrl(string url)
    {
        _webDriver.Navigate().GoToUrl(url);
    }

    public IWebElement GetWebElement(By by)
    {
        return _webDriver.FindElement(by);
    }

    public IEnumerable<IWebElement> GetWebElements(By by)
    {
        return _webDriver.FindElements(by);
    }

    public IWebElement TryGetWebElementUntil(By by, int timeout)
    {
        var wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(timeout));
        return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
    }

    public IEnumerable<IWebElement> TryGetWebElementsUntil(By by, int timeout)
    {
        var wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(timeout));
        return wait.Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(by)
        );
    }

    public void Quit()
    {
        _webDriver.Quit();
    }
}
