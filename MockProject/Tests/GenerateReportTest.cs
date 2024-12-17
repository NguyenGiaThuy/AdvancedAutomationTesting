namespace MockProject.Tests;

[TestClass]
public class GenerateReportTest : TestBase
{
    [TestInitialize]
    public void TestPrecondition()
    {
        try
        {
            _testContext.WriteLine($"Test precondition runs in {_testContext.FullyQualifiedTestClassName}");

            // Navigate to the Leave item in the sidebar
            _wait.Until(drv => drv.FindElement(By.XPath("//span[text()='Leave']/.."))).Click();

            // Navigate to the Reports item in the topbar
            _wait.Until(drv => drv.FindElement(By.XPath("//span[text()='Reports ']/.."))).Click();

            // Navigate to the My Leave Entitlements and Usage Report item
            // in the dropdown menu in topbar
            _wait.Until(drv => drv.FindElement(
                By.XPath("//a[text()='My Leave Entitlements and Usage Report']/.."))).Click();

            // Locate the h5 tag to verify that
            // the My Leave Entitlements and Usage Report h5 tag is in the page
            _wait.Until(drv => drv.FindElement(
                By.XPath("//h5[text()='My Leave Entitlements and Usage Report']")));
        }
        catch (NoSuchElementException ex)
        {
            throw new PreconditionException(
                "Failed to navigate to My Leave Entitlements and Usage Report page", ex);
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
        // Locate and click the Leave Period dropdown options
        _webDriver.FindElement(By.XPath("//div[@tabindex='0']/..")).Click();

        // Select a valid period from the dropodown options
        _webDriver.FindElement(By.XPath($"//span[text()='{period}']/..")).Click();

        // Verify if the period reflected after selecting a valid period from the dropdown options
        var actualPeriod = _webDriver.FindElement(By.XPath("//div[@tabindex='0']/..")).Text;
        Assert.AreEqual(expected: period, actual: actualPeriod);

        // Verify that error will not show
        Assert.ThrowsException<NoSuchElementException>(
            () => _webDriver.FindElement(By.XPath("//span[text()='Required']")));
    }

    /// <summary>
    /// TC_GENERATE_SECTION_02 - Verify thata the system validates the selected leave period.
    /// </summary>
    [TestMethod]
    public void TestSelectLeavePeriodUnsuccessfully()
    {
        // Locate and click the Leave Period dropdown options
        _webDriver.FindElement(By.XPath("//div[@tabindex='0']/..")).Click();

        // Select an invalid period from the dropdown options
        _webDriver.FindElement(By.XPath("//div[text()='-- Select --' and @role='option']")).Click();

        // Verify if the error will show
        var error = _webDriver.FindElement(By.XPath("//span[text()='Required']"));
        Assert.IsNotNull(error);
    }
}