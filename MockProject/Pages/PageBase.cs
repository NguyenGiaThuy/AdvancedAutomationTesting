namespace MockProject.Pages;

public class PageBase
{
    protected IBrowser _browser = null!;
    protected string _url = null!;

    protected PageBase(IBrowser browser, string url)
    {
        _browser = browser;
        _url = url;
    }

    /// <summary>
    /// Go to the page
    /// </summary>
    /// <exception cref="WebDriverException"></exception>
    public void GoToPage()
    {
        _browser.GoToUrl(_url);
    }
}
