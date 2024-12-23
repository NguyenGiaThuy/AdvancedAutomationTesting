namespace TestCore.Browsers;

public static class BrowserFactory
{
    public static IBrowser MakeBrowser(string launchBrowser, int implicitTimeout)
    {
        IDriverConfig driverConfig;
        IWebDriver webDriver;
        switch (launchBrowser)
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

        new WebDriverManager.DriverManager().SetUpDriver(driverConfig);
        webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(implicitTimeout);
        IBrowser browser = new Browser(webDriver);

        return browser;
    }
}
