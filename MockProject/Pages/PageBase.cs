namespace MockProject.Pages;

public class PageBase
{
    protected string _url = null!;

    protected IWebDriver _webDriver = null!;
    protected WebDriverWait _wait = null!;

    public PageBase(IWebDriver webDriver, WebDriverWait wait, string url)
    {
        _webDriver = webDriver;
        _wait = wait;
        _url = url;
    }

    public void NavigateToPage()
    {
        _webDriver.Navigate().GoToUrl(_url);
    }
}
