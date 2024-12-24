namespace MockProject.Pages.Home;

public class HomePage(IBrowser browser, string baseUrl)
    : PageBase(browser, $"{baseUrl}/web/index.php")
{
    #region PageElementLocators
    private By _profilePicture = By.XPath("//img[@alt='profile picture']");
    private By _leave = By.XPath("//span[text()='Leave']/..");
    private By _reports = By.XPath("//span[text()='Reports ']/..");
    private By _myLeaveBalanceReport = By.XPath(
        "//a[text()='My Leave Entitlements and Usage Report']/.."
    );
    #endregion

    #region PageInteractions
    public IWebElement GetProfilePicture()
    {
        var profilePicture = _browser.GetWebElement(_profilePicture);
        return profilePicture;
    }

    public void ClickLeave()
    {
        var leave = _browser.GetWebElement(_leave);
        leave.Click();
    }

    public void ClickReports()
    {
        var reports = _browser.GetWebElement(_reports);
        reports.Click();
    }

    public void ClickMyBalanceReport()
    {
        var myLeaveBalanceReport = _browser.GetWebElement(_myLeaveBalanceReport);
        myLeaveBalanceReport.Click();
    }

    public void GoToMyLeaveBalanceReportPage()
    {
        ClickLeave();
        ClickReports();
        ClickMyBalanceReport();
    }
    #endregion
}
