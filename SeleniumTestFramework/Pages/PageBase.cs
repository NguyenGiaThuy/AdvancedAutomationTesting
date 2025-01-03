namespace SeleniumTestFramework.Pages;

public class PageBase(IBrowser browser, string url)
{
    protected IBrowser _browser = browser;
    private string _url = url;

    /// <summary>
    /// Go to the page
    /// </summary>
    public void GoToPage()
    {
        _browser.GoToUrl(_url);
    }
}
