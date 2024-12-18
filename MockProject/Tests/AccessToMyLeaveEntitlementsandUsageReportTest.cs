namespace MockProject.Tests;

[TestClass]
public class AccessToMyLeaveEntitlementsandUsageReportTest : TestBase
{
    /// <summary>
    /// TC_PAGE_01 - Verify that the user can access the Leave Entitlements and Usage Report page.
    /// </summary>
    [TestMethod]
    public void TestAccessToMyLeaveEntitlementsandUsageReportSuccessfully()
    {
        // Navigate to the Leave item in the sidebar
        _webDriver.FindElement(By.XPath("//span[text()='Leave']/..")).Click();

        // Navigate to the Reports item in the topbar
        _webDriver.FindElement(By.XPath("//span[text()='Reports ']/..")).Click();

        // Navigate to the My Leave Entitlements and Usage Report item
        // in the dropdown menu in topbar
        _webDriver
            .FindElement(By.XPath("//a[text()='My Leave Entitlements and Usage Report']/.."))
            .Click();

        // Verify that the My Leave Entitlements and Usage Report h5 tag is in the page
        var h5 = _webDriver.FindElement(
            By.XPath("//h5[text()='My Leave Entitlements and Usage Report']")
        );
        Assert.IsNotNull(h5);
    }
}
