namespace TestCore.Browsers;

public static class BrowserFactory
{
    public static IBrowser MakeBrowser(
        string launchBrowser,
        int implicitTimeout,
        string? browserOptions
    )
    {
        IBrowser browser;
        switch (launchBrowser)
        {
            case "Firefox":
                browser = new FirefoxBrowser(implicitTimeout, browserOptions);
                break;
            case "Chrome":
                browser = new ChromeBrowser(implicitTimeout, browserOptions);
                break;
            case "Edge":
                browser = new EdgeBrowser(implicitTimeout, browserOptions);
                break;
            default:
                throw new WebDriverException("Invalid browser configuration");
        }

        return browser;
    }
}
