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
            // Navigate to the Home page
            _homePage = new HomePage(_browser);
            _homePage.GoToPage();

            // Navigate to the My Leave Entitlements and Usage Report page
            _myLeaveBalanceReportPage = new MyLeaveBalanceReportPage(_browser);
            _testContext.WriteLine(
                "Successfully navigated to the My Leave Entitlements and Usage Report page"
            );
        }
        catch (WebException ex)
        {
            _testContext.WriteLine("Failed to load page due to connection");
            throw new PreconditionException("Failed to load page due to connection", ex);
        }
    }

    [TestMethod(
        "TC_PAGE_01 - Verify that the user can access the Leave Entitlements and Usage Report page."
    )]
    [TestCategory("TC_PAGE")]
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
