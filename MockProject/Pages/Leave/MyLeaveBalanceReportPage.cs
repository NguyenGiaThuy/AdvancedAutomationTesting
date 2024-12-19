namespace MockProject.Pages.Leave;

public class MyLeaveBalanceReportPage : PageBase
{
    #region PageElements
    private IWebElement _title =>
        _webDriver.FindElement(By.XPath("//h5[text()='My Leave Entitlements and Usage Report']"));
    private IWebElement _dropdown => _webDriver.FindElement(By.XPath("//div[@tabindex='0']/.."));
    private IWebElement _dropdownOption = null!;
    private IWebElement _error => _webDriver.FindElement(By.XPath("//span[text()='Required']"));
    private IWebElement _generateButton =>
        _webDriver.FindElement(By.XPath("//button[contains(., 'Generate')]"));
    private IEnumerable<IWebElement> _dataColumn = null!;
    #endregion

    #region PageInteractions
    public MyLeaveBalanceReportPage(IWebDriver webDriver, WebDriverWait wait)
        : base(
            webDriver,
            wait,
            "https://opensource-demo.orangehrmlive.com/web/index.php/leave/viewMyLeaveBalanceReport"
        ) { }

    public IWebElement GetTitle()
    {
        return _title;
    }

    public void ClickDropdown()
    {
        _dropdown.Click();
    }

    public void SelectDropdownOption(int optionIdx)
    {
        var xpathPattern =
            optionIdx == 0
                ? "//div[text()='-- Select --' and @role='option']"
                : $"//div[@role='listbox']/div[{optionIdx + 1}]/span";
        _dropdownOption = _webDriver.FindElement(By.XPath(xpathPattern));

        _dropdownOption.Click();
    }

    public IWebElement GetError()
    {
        return _error;
    }

    public void ClickGenerateButton()
    {
        _generateButton.Click();
    }

    public IEnumerable<IWebElement> GetDataColumn(int colIdx, bool containsHeader = true)
    {
        string additionalContent;
        string xpathPattern;

        // Since the website uses bad coding convention,
        // we use this logic to workaround
        var actualColIdx = colIdx - 1;
        if (actualColIdx == -1)
        {
            additionalContent = containsHeader
                ? "(@class='rgCell' or string()='Leave Type')"
                : "@class='rgCell'";
            xpathPattern = $"//div[@data-rgcol='{actualColIdx + 1}' and {additionalContent}]";
        }
        else
        {
            additionalContent = containsHeader ? "" : " and @class!='rgHeaderCell'";
            xpathPattern =
                $"//div[@data-rgcol='{actualColIdx}' {additionalContent} and @class!='rgCell' and string()!='Leave Type']";
        }

        _wait.Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(xpathPattern))
        );
        _dataColumn = _webDriver.FindElements(By.XPath(xpathPattern));

        return _dataColumn;
    }
    #endregion
}
