namespace SeleniumTestFramework.Tests;

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
            TestContext.WriteLine(
                "Successfully navigated to the My Leave Entitlements and Usage Report page"
            );
        }
        catch (WebException ex)
        {
            TestContext.WriteLine("Failed to load page due to connection");
            throw new PreconditionException("Failed to load page due to connection", ex);
        }
    }

    [TestMethod("TC_GENERATE_SECTION_01 - Verify that the user can select a leave period.")]
    [TestCategory("TC_GENERATE_SECTION")]
    [TestCategory("SELENIUM")]
    [DataRow(1, DisplayName = "Second option")]
    [DataRow(-1, DisplayName = "Last option")]
    public void TestSelectLeavePeriodSuccessfully(int optionIdx)
    {
        // Step 1: Locate and click the Leave Period dropdown
        _myLeaveBalanceReportPage.ClickDropdown();

        // Step 2: Select a valid period from the dropodown
        _myLeaveBalanceReportPage.SelectDropdownOption(optionIdx);

        // Expected result: The error will not show up, which means the user has successfully selected a valid period
        Assert.ThrowsException<NoSuchElementException>(() => _myLeaveBalanceReportPage.GetError());
    }

    [TestMethod(
        "TC_GENERATE_SECTION_02 - Verify thata the system validates the selected leave period."
    )]
    [TestCategory("TC_GENERATE_SECTION")]
    [TestCategory("SELENIUM")]
    public void TestSelectLeavePeriodUnsuccessfully()
    {
        // Step 1: Locate and click the Leave Period dropdown
        _myLeaveBalanceReportPage.ClickDropdown();

        // Step 2: Select an invalid period from the dropdown
        _myLeaveBalanceReportPage.SelectDropdownOption(0);

        // Expected result: The error will show up, which means the user has selected an invalid period
        var error = _myLeaveBalanceReportPage.GetError();
        Assert.IsNotNull(error);
    }

    [TestMethod("TC_GENERATE_SECTION_03 - Verify thath the system allows report generation.")]
    [TestCategory("TC_GENERATE_SECTION")]
    [TestCategory("SELENIUM")]
    public void TestGenerateReportSuccessfully()
    {
        /* Since system automatically generates report for current period the first time
        we access the page, to verify the report generation, we have to work around by
        first evaluate current balance, then we change the period and evaluate balance
        of that report. Finally we compare the 2 balances to see if the new report is
        successfully generated.*/

        /* Precondition: Evaluate balance for current period */
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

        /* Test step(s): Evaluate balance for new period */
        // Step 1: Locate and click the leave Period dropdown
        _myLeaveBalanceReportPage.ClickDropdown();

        // Step 2: Select valid period from the dropdown
        _myLeaveBalanceReportPage.SelectDropdownOption(3);

        // Step 3: Locate and click the Generate button
        _myLeaveBalanceReportPage.ClickGenerateButton();

        // Step 4: Evaluate total Leave Balance (Days)
        var newBalance = _myLeaveBalanceReportPage
            .GetDataColumn(5, false)
            .Sum(item => decimal.Parse(item.Text));

        // Expected result: These balances are not equal, which means the new report is successfully generated
        Assert.AreNotEqual(currentBalance, newBalance);
    }

    [TestMethod(
        "TC_GENERATE_SECTION_04 - Verify that the system will not generate a report with invalid period."
    )]
    [TestCategory("TC_GENERATE_SECTION")]
    [TestCategory("SELENIUM")]
    public void TestGenerateReportUnsuccessfully()
    {
        /* Since system automatically generates report for current period the first time
        we access the page, to verify the report is not generated, we have to work around by
        first evaluate current balance, then we select an invalid period and evaluate balance
        of that report. Finally we compare the 2 balances to see if the they are equal.*/

        /* Precondition: Evaluate balance for current period */
        var currentBalance = _myLeaveBalanceReportPage
            .GetDataColumn(5, false)
            .Sum(item => decimal.Parse(item.Text));

        /* Test step(s): Evaluate balance for an invalid period */
        // Step 1: Locate and click the Leave Period dropdown
        _myLeaveBalanceReportPage.ClickDropdown();

        // Step 2: Select an invalid period from the dropdown
        _myLeaveBalanceReportPage.SelectDropdownOption(0);

        // Step 3: Locate and click the Generate button
        _myLeaveBalanceReportPage.ClickGenerateButton();

        // Step 4: Evaluate new total Leave Balance (Days)
        var newBalance = _myLeaveBalanceReportPage
            .GetDataColumn(5, false)
            .Sum(item => decimal.Parse(item.Text));

        // Expected result: These balances are equal, which means the new report is not generated
        Assert.AreEqual(currentBalance, newBalance);
    }

    [TestMethod(
        "TC_GENERATE_SECTION_05 - Verify that the section containing Generate button can collapse."
    )]
    [TestCategory("TC_GENERATE_SECTION")]
    [TestCategory("SELENIUM")]
    public void TestCollapseGenerateSectionSuccessfully()
    {
        // Locate and click the Collapse button
        _myLeaveBalanceReportPage.ClickCollapseButton();

        // Expected result: The Generate button is not clickable, which means the section has collapsed
        var clicked = _myLeaveBalanceReportPage.GenerateButtonIsClickable();
        Assert.IsFalse(clicked);
    }

    [TestMethod(
        "TC_GENERATE_SECTION_06 - Verify that the section containing Generate button can expand."
    )]
    [TestCategory("TC_GENERATE_SECTION")]
    [TestCategory("SELENIUM")]
    public void TestExpandGenerateSectionSuccessfully()
    {
        /* Precondition: The section should have been collapsed*/
        // Locate and click the Collapse button
        _myLeaveBalanceReportPage.ClickCollapseButton();

        /* Test step(s) */
        // Locate and click the Expand button
        _myLeaveBalanceReportPage.ClickExpandButton();

        // Expected result: The Generate button is clickable, which means the section has expanded
        var clicked = _myLeaveBalanceReportPage.GenerateButtonIsClickable();
        Assert.IsTrue(clicked);
    }
}
