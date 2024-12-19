namespace MockProject.Tests;

[TestClass]
public class GenerateReportTest : TestBase
{
    private MyLeaveBalanceReportPage _myLeaveBalanceReportPage = null!;

    [TestInitialize]
    public void TestPrecondition()
    {
        try
        {
            // Navigate to the My Leave Entitlements and Usage Report page
            _myLeaveBalanceReportPage = new MyLeaveBalanceReportPage(_browser);
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

    /// <summary>
    /// TC_GENERATE_SECTION_01 - Verify that the user can select a leave period.
    /// </summary>
    /// <param name="period"></param>
    [TestMethod("TC_GENERATE_SECTION_01 - Verify that the user can select a leave period.")]
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

    /// <summary>
    /// TC_GENERATE_SECTION_02 - Verify thata the system validates the selected leave period.
    /// </summary>
    [TestMethod(
        "TC_GENERATE_SECTION_02 - Verify thata the system validates the selected leave period."
    )]
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

    /// <summary>
    /// TC_GENERATE_SECTION_03 - Verify thath the system allows report generation.
    /// with a valid leave period.
    /// </summary>
    [TestMethod("TC_GENERATE_SECTION_03 - Verify thath the system allows report generation.")]
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
}
