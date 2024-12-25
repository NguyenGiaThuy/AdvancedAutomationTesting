namespace TestCore.Browsers;

public static class BrowserFactory
{
    public static IBrowser MakeBrowser(
        BrowserEnum browserType,
        int implicitTimeout,
        string? browserOptions
    )
    {
        switch (browserType)
        {
            case BrowserEnum.Firefox:
                return new FirefoxBrowser(implicitTimeout, browserOptions);
            case BrowserEnum.Chrome:
                return new ChromeBrowser(implicitTimeout, browserOptions);
            case BrowserEnum.Edge:
                return new EdgeBrowser(implicitTimeout, browserOptions);
            default:
                throw new WebDriverException("Invalid browser configuration");
        }
    }
}
