namespace TestCore.Browsers;

public class FirefoxBrowser : IBrowser
{
    private IWebDriver _webDriver = null!;

    public FirefoxBrowser(int implicitTimeout, string? browserOptions)
    {
        new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());

        if (browserOptions is not null)
        {
            var options = new FirefoxOptions();
            options.AddArgument(browserOptions);
            _webDriver = new FirefoxDriver(options);
        }
        else
        {
            _webDriver = new FirefoxDriver();
        }

        _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(implicitTimeout);
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

    public bool WebElementIsVisibile(By by, int timeout = 1000)
    {
        try
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(timeout));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public bool WebElementIsClickable(By by, int timeout = 1000)
    {
        try
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromMilliseconds(timeout));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public void Quit()
    {
        _webDriver.Quit();
    }
}
