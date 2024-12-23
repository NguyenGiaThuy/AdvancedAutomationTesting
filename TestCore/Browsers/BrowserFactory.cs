namespace TestCore.Browsers;

public static class BrowserFactory
{
    public static IBrowser MakeBrowser(string launchBrowser, int implicitTimeout)
    {
        IWebDriver webDriver;
        switch (launchBrowser)
        {
            case "Firefox":
                new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                webDriver = new FirefoxDriver();
                break;
            case "Chrome":
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                webDriver = new ChromeDriver();
                break;
            case "Edge":
                new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                webDriver = new EdgeDriver();
                break;
            default:
                throw new WebDriverException("Invalid browser configuration");
        }

        webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(implicitTimeout);
        IBrowser browser = new Browser(webDriver);

        return browser;
    }
}
