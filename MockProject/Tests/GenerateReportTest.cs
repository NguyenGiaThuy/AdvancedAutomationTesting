namespace MockProject.Tests;

[TestClass]
public class GenerateReportTest : TestBase
{
    private MyLeaveBalanceReportPage _page = default!;

    [TestInitialize]
    public void TestPrecondition()
    {
        try
        {
            _testContext.WriteLine(
                $"Test precondition runs in {_testContext.FullyQualifiedTestClassName}"
            );

            // Navigate to the My Leave Entitlements and Usage Report page
            _page = new MyLeaveBalanceReportPage(
                _webDriver,
                _wait,
                "https://opensource-demo.orangehrmlive.com/web/index.php/leave/viewMyLeaveBalanceReport"
            );
        }
        catch (WebException ex)
        {
            throw new PreconditionException("Failed to load page due to connection", ex);
        }
        catch (TimeoutException ex)
        {
            throw new PreconditionException("Failed to load page due to timeout", ex);
        }
        catch (NoSuchElementException ex)
        {
            throw new PreconditionException(
                "Failed to navigate to My Leave Entitlements and Usage Report page",
                ex
            );
        }
    }

    /// <summary>
    /// TC_GENERATE_SECTION_01 - Verify that the user can select a leave period.
    /// </summary>
    /// <param name="period"></param>
    [TestMethod]
    [DataRow("2020-01-01 - 2020-31-12")]
    [DataRow("2025-01-01 - 2025-31-12")]
    public void TestSelectLeavePeriodSuccessfully(string period)
    {
        // Locate and click the Leave Period dropdown
        _page.ClickDropdown();

        // Select a valid period from the dropodown
        _page.SelectDropdownOption(
            By.XPath($"//span[normalize-space(text())='{period}']/..[@role='option']")
        );

        // Verify if the period reflected after selecting a valid period from the dropdown
        var actualPeriod = _page.GetDropdownText();
        Assert.AreEqual(expected: period, actual: actualPeriod);

        // Verify that error will not show
        Assert.ThrowsException<NoSuchElementException>(() => _page.GetError());
    }

    /// <summary>
    /// TC_GENERATE_SECTION_02 - Verify thata the system validates the selected leave period.
    /// </summary>
    [TestMethod]
    public void TestSelectLeavePeriodUnsuccessfully()
    {
        // Locate and click the Leave Period dropdown
        _page.ClickDropdown();

        // Select an invalid period from the dropdown
        _page.SelectDropdownOption(By.XPath("//div[text()='-- Select --' and @role='option']"));

        // Verify if the error will show
        var error = _page.GetError();
        Assert.IsNotNull(error);
    }

    /// <summary>
    /// TC_GENERATE_SECTION_03 - Verify thath the system allows report generation
    /// with a valid leave period.
    /// </summary>
    [TestMethod]
    public void TestGenerateReportSuccessfully()
    {
        // Locate and click the leave Period dropdown
        _page.ClickDropdown();

        // Select a valid period from the dropdown
        _page.SelectDropdownOption(
            By.XPath("//span[normalize-space(text())='2020-01-01 - 2020-31-12']/..[@role='option']")
        );

        // Locate and click the Generate button
        _page.ClickGenerateButton();

        // Evaluate current total Leave Balance (Days)
        var currentBalance = _page.GetDataColumn(5, false).Sum(item => decimal.Parse(item.Text));

        // Locate and click the leave Period dropdown again
        _page.ClickDropdown();

        // Select another valid period from the dropdown
        _page.SelectDropdownOption(
            By.XPath("//span[normalize-space(text())='2024-01-01 - 2024-31-12']/..[@role='option']")
        );

        // Locate and click the Generate button again
        _page.ClickGenerateButton();

        // Evaluate new total Leave Balance (Days)
        var newBalance = _page.GetDataColumn(5, false).Sum(item => decimal.Parse(item.Text));

        // Verify that these balances are not equal
        Assert.AreNotEqual(currentBalance, newBalance);
    }
}
