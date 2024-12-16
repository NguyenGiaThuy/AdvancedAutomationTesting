using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MockProject.Tests;

[TestClass]
public class AccessToMyLeaveEntitlementsandUsageReportTest : BaseTest
{
    [TestMethod("Verify that the user can access the Leave Entitlements and Usage Report page.")]
    public void TestAccessToMyLeaveEntitlementsandUsageReportSuccessfully()
    {
        // Navigate to the Leave item in the sidebar
        new WebDriverWait(m_webDriver, TimeSpan.FromSeconds(m_timeout))
            .Until(drv => drv.FindElement(By.XPath("//span[text()='Leave']/.."))).Click();

        // Navigate to the Reports item in the topbar
        new WebDriverWait(m_webDriver, TimeSpan.FromSeconds(m_timeout))
            .Until(drv => drv.FindElement(By.XPath("//span[text()='Reports ']/.."))).Click();

        // Navigate to the My Leave Entitlements and Usage Report item
        // in the dropdown menu in topbar
        new WebDriverWait(m_webDriver, TimeSpan.FromSeconds(m_timeout))
            .Until(drv => drv.FindElement(
                By.XPath("//a[text()='My Leave Entitlements and Usage Report']/.."))).Click();

        // Verify that the My Leave Entitlements and Usage Report h5 tag is in the page
        var actual = new WebDriverWait(m_webDriver, TimeSpan.FromSeconds(m_timeout))
            .Until(drv => drv.FindElement(
                By.XPath("//h5[text()='My Leave Entitlements and Usage Report']")));

        Assert.IsNotNull(actual);
    }
}