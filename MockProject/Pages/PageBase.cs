namespace MockProject.Pages;

public class PageBase
{
    protected IWebDriver _webDriver = default!;
    protected WebDriverWait _wait = default!;

    public PageBase(IWebDriver webDriver, WebDriverWait wait, string url)
    {
        _webDriver = webDriver;
        _wait = wait;
        _webDriver.Navigate().GoToUrl(url);
    }
}
