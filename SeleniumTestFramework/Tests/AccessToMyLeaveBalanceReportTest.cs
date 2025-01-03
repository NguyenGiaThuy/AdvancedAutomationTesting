namespace SeleniumTestFramework.Tests;

[TestClass]
public class AccessToMyLeaveBalanceReportTest : TestBase
{
    private HomePage _homePage = null!;
    private MyLeaveBalanceReportPage _myLeaveBalanceReportPage = null!;

    [TestInitialize]
    public override void TestPrecondition()
    {
        base.TestPrecondition();

        try
        {
            // Navigate to the Home page
            _homePage = new HomePage(_browser, _baseUrl);
            _homePage.GoToPage();

            // Navigate to the My Leave Entitlements and Usage Report page
            _myLeaveBalanceReportPage = new MyLeaveBalanceReportPage(_browser, _baseUrl);
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
    [TestCategory("SELENIUM")]
    public void TestAccessToMyLeaveBalanceReportSuccessfully()
    {
        // Step 1: Navigate to the Leave item in the sidebar
        _homePage.ClickLeave();

        // Step 2: Navigate to the Reports item in the topbar
        _homePage.ClickReports();

        // Step 3: Navigate to the My Leave Entitlements and Usage Report item
        // in the dropdown menu in topbar
        _homePage.ClickMyBalanceReport();

        // Expected result: The My Leave Entitlements and Usage Report title is in the page,
        // which means the user has successfully accessed the page
        var title = _myLeaveBalanceReportPage.GetTitle();
        Assert.IsNotNull(title);
    }
}
