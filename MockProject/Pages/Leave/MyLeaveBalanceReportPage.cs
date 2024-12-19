namespace MockProject.Pages.Leave;

public class MyLeaveBalanceReportPage(IBrowser browser)
    : PageBase(
        browser,
        "https://opensource-demo.orangehrmlive.com/web/index.php/leave/viewMyLeaveBalanceReport"
    )
{
    #region PageElementLocators
    private By _title = By.XPath("//h5[text()='My Leave Entitlements and Usage Report']");
    private By _dropdown => By.XPath("//div[@tabindex='0']/..");
    private By _error => By.XPath("//span[text()='Required']");
    private By _generateButton = By.XPath("//button[contains(., 'Generate')]");
    #endregion

    #region PageInteractions
    public IWebElement GetTitle()
    {
        var title = _browser.GetWebElement(_title);
        return title;
    }

    public IWebElement GetError()
    {
        var error = _browser.GetWebElement(_error);
        return error;
    }

    public void ClickDropdown()
    {
        var dropdown = _browser.GetWebElement(_dropdown);
        dropdown.Click();
    }

    public void SelectDropdownOption(int optionIdx)
    {
        string xpathPattern;
        switch (optionIdx)
        {
            case -1:
                xpathPattern = $"//div[@role='listbox']/div[last()]";
                break;
            case 0: // Workaround because of bad coding
                xpathPattern = "//div[text()='-- Select --' and @role='option']";
                break;
            default:
                xpathPattern = $"//div[@role='listbox']/div[{optionIdx + 1}]";
                break;
        }

        var dropdownOption = _browser.GetWebElement(By.XPath(xpathPattern));
        dropdownOption.Click();
    }

    public void ClickGenerateButton()
    {
        var generateButton = _browser.GetWebElement(_generateButton);
        generateButton.Click();
    }

    public IEnumerable<IWebElement> GetDataColumn(int colIdx, bool containsHeader = true)
    {
        // Since the website uses bad coding,
        // we use this logic to workaround
        string additionalContent;
        string xpathPattern;
        var actualColIdx = colIdx - 1;
        switch (actualColIdx)
        {
            case -1:
                additionalContent = containsHeader
                    ? "(@class='rgCell' or string()='Leave Type')"
                    : "@class='rgCell'";
                xpathPattern = $"//div[@data-rgcol='{actualColIdx + 1}' and {additionalContent}]";
                break;
            default:
                additionalContent = containsHeader ? "" : " and @class!='rgHeaderCell'";
                xpathPattern =
                    $"//div[@data-rgcol='{actualColIdx}' {additionalContent} and @class!='rgCell' and string()!='Leave Type']";
                break;
        }

        var dataColumn = _browser.TryGetWebElementsUntil(By.XPath(xpathPattern), 30000);
        return dataColumn;
    }
    #endregion
}
