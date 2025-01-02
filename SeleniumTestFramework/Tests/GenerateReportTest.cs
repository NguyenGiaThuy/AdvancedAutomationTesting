namespace MockProject.Tests;

[TestClass]
public class GenerateReportTest : TestBase
{
    private MyLeaveBalanceReportPage _myLeaveBalanceReportPage = null!;

    [TestInitialize]
    public override void TestPrecondition()
    {
        base.TestPrecondition();

        try
        {
            // Navigate to the My Leave Entitlements and Usage Report page
            _myLeaveBalanceReportPage = new MyLeaveBalanceReportPage(_browser, _baseUrl);
            _myLeaveBalanceReportPage.GoToPage();
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

    [TestMethod("TC_GENERATE_SECTION_01 - Verify that the user can select a leave period.")]
    [TestCategory("TC_GENERATE_SECTION")]
    [DataRow(1, DisplayName = "Second option")]
    [DataRow(-1, DisplayName = "Last option")]
    public void TestSelectLeavePeriodSuccessfully(int optionIdx)
    {
        // Locate and click the Leave Period dropdown
        _myLeaveBalanceReportPage.ClickDropdown();

        // Select a valid period from the dropodown
        _myLeaveBalanceReportPage.SelectDropdownOption(optionIdx);

        // Verify that error will not show
        Assert.ThrowsException<NoSuchElementException>(() => _myLeaveBalanceReportPage.GetError());
    }

    [TestMethod(
        "TC_GENERATE_SECTION_02 - Verify thata the system validates the selected leave period."
    )]
    [TestCategory("TC_GENERATE_SECTION")]
    public void TestSelectLeavePeriodUnsuccessfully()
    {
        // Locate and click the Leave Period dropdown
        _myLeaveBalanceReportPage.ClickDropdown();

        // Select an invalid period from the dropdown
        _myLeaveBalanceReportPage.SelectDropdownOption(0);

        // Verify if the error will show
        var error = _myLeaveBalanceReportPage.GetError();
        Assert.IsNotNull(error);
    }

    [TestMethod("TC_GENERATE_SECTION_03 - Verify thath the system allows report generation.")]
    [TestCategory("TC_GENERATE_SECTION")]
    public void TestGenerateReportSuccessfully()
    {
        // Locate and click the leave Period dropdown
        _myLeaveBalanceReportPage.ClickDropdown();

        // Select a valid period from the dropdown
        _myLeaveBalanceReportPage.SelectDropdownOption(1);

        // Locate and click the Generate button
        _myLeaveBalanceReportPage.ClickGenerateButton();

        // Evaluate current total Leave Balance (Days)
        var currentBalance = _myLeaveBalanceReportPage
            .GetDataColumn(5, false)
            .Sum(item => decimal.Parse(item.Text));

        // Locate and click the leave Period dropdown again
        _myLeaveBalanceReportPage.ClickDropdown();

        // Select another valid period from the dropdown
        _myLeaveBalanceReportPage.SelectDropdownOption(3);

        // Locate and click the Generate button again
        _myLeaveBalanceReportPage.ClickGenerateButton();

        // Evaluate new total Leave Balance (Days)
        var newBalance = _myLeaveBalanceReportPage
            .GetDataColumn(5, false)
            .Sum(item => decimal.Parse(item.Text));

        // Verify that these balances are not equal
        Assert.AreNotEqual(currentBalance, newBalance);
    }

    [TestMethod(
        "TC_GENERATE_SECTION_04 - Verify that the system will not generate a report with invalid period."
    )]
    [TestCategory("TC_GENERATE_SECTION")]
    public void TestGenerateReportUnsuccessfully()
    {
        // Locate and click the Leave Period dropdown
        _myLeaveBalanceReportPage.ClickDropdown();

        // Select an invalid period from the dropdown
        _myLeaveBalanceReportPage.SelectDropdownOption(0);

        // Evaluate current total Leave Balance (Days)
        var currentBalance = _myLeaveBalanceReportPage
            .GetDataColumn(5, false)
            .Sum(item => decimal.Parse(item.Text));

        // Locate and click the Generate button
        _myLeaveBalanceReportPage.ClickGenerateButton();

        // Evaluate new total Leave Balance (Days)
        var newBalance = _myLeaveBalanceReportPage
            .GetDataColumn(5, false)
            .Sum(item => decimal.Parse(item.Text));

        // Verify that these balances are equal
        Assert.AreEqual(currentBalance, newBalance);
    }

    [TestMethod(
        "TC_GENERATE_SECTION_05 - Verify that the section containing Generate button can collapse."
    )]
    [TestCategory("TC_GENERATE_SECTION")]
    public void TestCollapseGenerateSectionSuccessfully()
    {
        // Locate and click the Collapse button
        _myLeaveBalanceReportPage.ClickCollapseButton();

        // Verify that the Generate button is not clickable
        var clicked = _myLeaveBalanceReportPage.GenerateButtonIsClickable();
        Assert.IsFalse(clicked);
    }

    [TestMethod(
        "TC_GENERATE_SECTION_06 - Verify that the section containing Generate button can expand."
    )]
    [TestCategory("TC_GENERATE_SECTION")]
    public void TestExpandGenerateSectionSuccessfully()
    {
        // Locate and click the Collapse button
        _myLeaveBalanceReportPage.ClickCollapseButton();

        // Locate and click the Expand button
        _myLeaveBalanceReportPage.ClickExpandButton();

        // Verify that the Generate button is clickable
        var clicked = _myLeaveBalanceReportPage.GenerateButtonIsClickable();
        Assert.IsTrue(clicked);
    }
}
