namespace MockProject.Pages.Home;

public class HomePage : PageBase
{
    #region PageElements
    private IWebElement _profilePicture =>
        _webDriver.FindElement(By.XPath("//img[@alt='profile picture']"));
    private IWebElement _leave => _webDriver.FindElement(By.XPath("//span[text()='Leave']/.."));
    private IWebElement _reports =>
        _webDriver.FindElement(By.XPath("//span[text()='Reports ']/.."));
    private IWebElement _myLeaveBalanceReport =>
        _webDriver.FindElement(By.XPath("//a[text()='My Leave Entitlements and Usage Report']/.."));
    #endregion

    #region PageInteractions
    public HomePage(IWebDriver webDriver, WebDriverWait wait)
        : base(webDriver, wait, "https://opensource-demo.orangehrmlive.com/web/index.php") { }

    public IWebElement GetProfilePicture()
    {
        return _profilePicture;
    }

    public void ClickLeave()
    {
        _leave.Click();
    }

    public void ClickReports()
    {
        _reports.Click();
    }

    public void ClickMyBalanceReport()
    {
        _myLeaveBalanceReport.Click();
    }
    #endregion
}
