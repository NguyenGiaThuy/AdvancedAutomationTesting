namespace TestCore.Browsers;

public static class BrowserFactory
{
    public static IBrowser MakeBrowser(
        BrowserType browserType,
        int implicitTimeout,
        string? browserOptions
    )
    {
        switch (browserType)
        {
            case BrowserType.Firefox:
                return new FirefoxBrowser(implicitTimeout, browserOptions);
            case BrowserType.Chrome:
                return new ChromeBrowser(implicitTimeout, browserOptions);
            case BrowserType.Edge:
                return new EdgeBrowser(implicitTimeout, browserOptions);
            default:
                throw new WebDriverException("Invalid browser configuration");
        }
    }
}
