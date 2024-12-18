namespace MockProject.Pages;

public class MyLeaveBalanceReportPage : PageBase
{
    private IWebElement _dropdown => _webDriver.FindElement(By.XPath("//div[@tabindex='0']/.."));
    private IWebElement _dropdownOption = default!;
    private IWebElement _error => _webDriver.FindElement(By.XPath("//span[text()='Required']"));
    private IWebElement _generateButton =>
        _webDriver.FindElement(By.XPath("//button[contains(., 'Generate')]"));
    private IEnumerable<IWebElement> _dataColumn = default!;

    public MyLeaveBalanceReportPage(IWebDriver webDriver, WebDriverWait wait, string url)
        : base(webDriver, wait, url) { }

    public void ClickDropdown()
    {
        _dropdown.Click();
    }

    public void SelectDropdownOption(By selector)
    {
        _dropdownOption = _webDriver.FindElement(selector);
        _dropdownOption.Click();
    }

    public string GetDropdownText()
    {
        return _dropdown.Text;
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
}
