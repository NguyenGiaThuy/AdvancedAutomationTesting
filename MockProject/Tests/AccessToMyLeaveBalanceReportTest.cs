namespace MockProject.Tests;

[TestClass]
public class AccessToMyLeaveBalanceReportTest : TestBase
{
    private HomePage _homePage = null!;
    private MyLeaveBalanceReportPage _myLeaveBalanceReportPage = null!;

    [TestInitialize]
    public void TestPrecondition()
    {
        try
        {
            _testContext.WriteLine(
                $"Test precondition runs in {_testContext.FullyQualifiedTestClassName}"
            );

            // Navigate to the Home page
            _homePage = new HomePage(_webDriver, _wait);
            _homePage.NavigateToPage();

            // Navigate to the My Leave Entitlements and Usage Report page
            _myLeaveBalanceReportPage = new MyLeaveBalanceReportPage(_webDriver, _wait);
        }
        catch (WebException ex)
        {
            throw new PreconditionException("Failed to load page due to connection", ex);
        }
        catch (TimeoutException ex)
        {
            throw new PreconditionException("Failed to load page due to timeout", ex);
        }
    }

    /// <summary>
    /// TC_PAGE_01 - Verify that the user can access the Leave Entitlements and Usage Report page.
    /// </summary>
    [TestMethod]
    public void TestAccessToMyLeaveBalanceReportSuccessfully()
    {
        // Navigate to the Leave item in the sidebar
        _homePage.ClickLeave();

        // Navigate to the Reports item in the topbar
        _homePage.ClickReports();

        // Navigate to the My Leave Entitlements and Usage Report item
        // in the dropdown menu in topbar
        _homePage.ClickMyBalanceReport();

        // Verify that the My Leave Entitlements and Usage Report title is in the page
        var title = _myLeaveBalanceReportPage.GetTitle();
        Assert.IsNotNull(title);
    }
}
