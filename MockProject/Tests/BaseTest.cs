using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;

namespace MockProject.Tests;

[TestClass]
public class BaseTest
{
    protected static TestContext m_testContext = default!;
    protected static IWebDriver m_webDriver = default!;

    [ClassInitialize]
    public static void ClassPrecondition(TestContext testContext)
    {
        m_testContext = testContext;

        new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
        m_webDriver = new FirefoxDriver();

        m_webDriver.Url = "https://opensource-demo.orangehrmlive.com";
        m_webDriver.FindElement(By.XPath("//input[@name='username']")).SendKeys("Admin");
        m_webDriver.FindElement(By.XPath("//input[@name='password']")).SendKeys("admin123");
        m_webDriver.FindElement(By.XPath("//button[@class='orangehrm-login-button']")).Click();
    }

    [ClassCleanup]
    public static void ClassPostcondition()
    {
        m_webDriver.Quit();
    }
}