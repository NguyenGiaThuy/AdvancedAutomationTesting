namespace MockProject.Pages.Leave.Configure;

public class LeaveTypesPage(IBrowser browser)
    : PageBase(
        browser,
        "https://opensource-demo.orangehrmlive.com/web/index.php/leave/leaveTypeList"
    )
{
    #region PageElementLocators
    private By _addButton = By.XPath("//button[contains(.,  'Add')]");
    private By _leaveNameInput = By.XPath("//div[@class='']/input");
    private By _saveButton = By.XPath("//button[contains(., 'Save')]");
    private By _confirmDeleteButton = By.XPath("//button[contains(., 'Yes, Delete')]");
    #endregion

    #region PageInteractions
    public bool DeleteLeaveTypeButtonIsVisible(string name)
    {
        return _browser.WebElementIsVisibile(
            By.XPath($"//div[contains(text(), '{name}')]/../..//button[1]"),
            10000
        );
    }

    public string GetLeaveTypeNameByIndex(int idx)
    {
        var xpathPattern = idx == -1 ? "last()" : (idx + 1).ToString();
        var leaveType = _browser.GetWebElement(
            By.XPath(
                $"//div[@class='orangehrm-container']//div[@class='oxd-table-body']/div[{xpathPattern}]//div[@data-v-6c07a142='']"
            )
        );

        return leaveType.Text;
    }

    public void ClickDeleteLeaveTypeButtonByIndex(int idx)
    {
        var deleteLeaveTypeButton = _browser.GetWebElement(
            By.XPath(
                $"//div[@class='orangehrm-container']//div[@class='oxd-table-body']/div[{idx + 1}]//button[1]"
            )
        );
        deleteLeaveTypeButton.Click();
    }

    public void ClickDeleteLeaveTypeButtonByName(string name)
    {
        var deleteLeaveTypeButton = _browser.GetWebElement(
            By.XPath($"//div[contains(text(), '{name}')]/../..//button[1]")
        );
        deleteLeaveTypeButton.Click();
    }

    public bool AddButtonIsVisible()
    {
        return _browser.WebElementIsVisibile(_addButton);
    }

    public void ClickAddButton()
    {
        var addButton = _browser.GetWebElement(_addButton);
        addButton.Click();
    }

    public void EnterLeaveName(string name)
    {
        var leaveNameInput = _browser.GetWebElement(_leaveNameInput);
        leaveNameInput.SendKeys(name);
    }

    public void ClickSaveButton()
    {
        var saveButton = _browser.GetWebElement(_saveButton);
        saveButton.Click();
    }

    public void ClickConfirmDeleteButton()
    {
        var confirmDeleteButton = _browser.GetWebElement(_confirmDeleteButton);
        confirmDeleteButton.Click();
    }
    #endregion
}
