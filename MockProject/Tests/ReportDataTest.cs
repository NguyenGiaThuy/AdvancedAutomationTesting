namespace MockProject.Tests;

[TestClass]
public class ReportDataTest : TestBase
{
    private MyLeaveBalanceReportPage _myLeaveBalanceReportPage = null!;

    private string DeleteLeaveTypePrecondition()
    {
        try
        {
            // Navigate to the Leave Types page
            var leaveTypesPage = new LeaveTypesPage(_browser);
            leaveTypesPage.GoToPage();
            _testContext.WriteLine("Successfully navigated to the Leave Types page");

            // Get the first Leave Type name
            var leaveTypeName = leaveTypesPage.GetLeaveTypeNameByIndex(0);

            // Delete the first Leave Type
            leaveTypesPage.ClickDeleteLeaveTypeButtonByIndex(0);
            leaveTypesPage.ClickConfirmDeleteButton();
            _testContext.WriteLine($"Successfully deleted the leave type: {leaveTypeName}");

            // Navigate to the My Leave Entitlements and Usage Report page
            _myLeaveBalanceReportPage = new MyLeaveBalanceReportPage(_browser);
            _myLeaveBalanceReportPage.GoToPage();
            _testContext.WriteLine(
                "Successfully navigated to the My Leave Entitlements and Usage Report page"
            );

            return leaveTypeName;
        }
        catch (WebException ex)
        {
            _testContext.WriteLine("Failed to load page due to connection");
            throw new PreconditionException("Failed to load page due to connection", ex);
        }
        catch (NoSuchElementException ex)
        {
            _testContext.WriteLine("Failed to find the leave type to delete");
            throw new PreconditionException("Failed to find the leave type to delete", ex);
        }
    }

    private void DeleteLeaveTypePostcondition(string leaveTypeName)
    {
        try
        {
            // Navigate to the Leave Types page
            var leaveTypesPage = new LeaveTypesPage(_browser);
            leaveTypesPage.GoToPage();
            _testContext.WriteLine("Successfully navigated to the Leave Types page");

            // Add a new leave type
            leaveTypesPage.ClickAddButton();
            leaveTypesPage.EnterLeaveName(leaveTypeName);
            leaveTypesPage.ClickSaveButton();

            var visible = leaveTypesPage.DeleteLeaveTypeButtonIsVisible(leaveTypeName);
            if (visible)
            {
                _testContext.WriteLine($"Successfully recovered leave type: {leaveTypeName}");
            }
            else
            {
                _testContext.WriteLine($"Failed to recover the leave type: {leaveTypeName}");
                throw new PostconditionException(
                    $"Failed to recover the leave type: {leaveTypeName}"
                );
            }
        }
        catch (WebException ex)
        {
            _testContext.WriteLine("Failed to load page due to connection");
            throw new PostconditionException("Failed to load page due to connection", ex);
        }
        catch (NoSuchElementException ex)
        {
            _testContext.WriteLine($"Failed to find recovered type: {leaveTypeName}");
            throw new PostconditionException($"Failed to find recovered type: {leaveTypeName}", ex);
        }
    }

    private string AddLeaveTypePrecondition()
    {
        try
        {
            // Create new Leave Type name
            var newLeaveTypeName = Guid.NewGuid().ToString();

            // Navigate to the Leave Types page
            var leaveTypesPage = new LeaveTypesPage(_browser);
            leaveTypesPage.GoToPage();
            _testContext.WriteLine("Successfully navigated to the Leave Types page");

            // Add a new leave type
            leaveTypesPage.ClickAddButton();
            leaveTypesPage.EnterLeaveName(newLeaveTypeName);
            leaveTypesPage.ClickSaveButton();

            var visible = leaveTypesPage.DeleteLeaveTypeButtonIsVisible(newLeaveTypeName);
            if (visible)
            {
                _testContext.WriteLine("Successfully added a new leave type");
            }
            else
            {
                _testContext.WriteLine("Failed to add a new leave type");
                throw new PreconditionException("Failed to add a new leave type");
            }

            // Navigate to the My Leave Entitlements and Usage Report page
            _myLeaveBalanceReportPage = new MyLeaveBalanceReportPage(_browser);
            _myLeaveBalanceReportPage.GoToPage();
            _testContext.WriteLine(
                "Successfully navigated to the My Leave Entitlements and Usage Report page"
            );

            return newLeaveTypeName;
        }
        catch (WebException ex)
        {
            _testContext.WriteLine("Failed to load page due to connection");
            throw new PreconditionException("Failed to load page due to connection", ex);
        }
        catch (NoSuchElementException ex)
        {
            _testContext.WriteLine("Failed to find the leave type to add");
            throw new PreconditionException("Failed to find the leave type to add", ex);
        }
    }

    private void AddLeaveTypePostcondition(string leaveTypeName)
    {
        try
        {
            // Navigate to the Leave Types page
            var leaveTypesPage = new LeaveTypesPage(_browser);
            leaveTypesPage.GoToPage();
            _testContext.WriteLine("Successfully navigated to the Leave Types page");

            // Delete the new Leave Type
            leaveTypesPage.ClickDeleteLeaveTypeButtonByName(leaveTypeName);
            leaveTypesPage.ClickConfirmDeleteButton();
            _testContext.WriteLine($"Successfully deleted the new leave type: {leaveTypeName}");
        }
        catch (WebException ex)
        {
            _testContext.WriteLine("Failed to load page due to connection");
            throw new PostconditionException("Failed to load page due to connection", ex);
        }
        catch (NoSuchElementException ex)
        {
            _testContext.WriteLine($"Failed to find the new leave type to delete: {leaveTypeName}");
            throw new PostconditionException(
                $"Failed to find the new leave type to delete: {leaveTypeName}",
                ex
            );
        }
    }

    [TestMethod(
        "TC_DATA_SECTION_06 - Verify that the deleted value of Leave Type does not show in the data table."
    )]
    [TestCategory("TC_DATA_SECTION")]
    public void TestDeletedLeaveTypeDoesnotShowSuccessfully()
    {
        // Delete the leave type
        var deletedLeaveTypeName = DeleteLeaveTypePrecondition();

        // Generate a random report
        _myLeaveBalanceReportPage.ClickDropdown();
        _myLeaveBalanceReportPage.SelectDropdownOption(1);
        _myLeaveBalanceReportPage.ClickGenerateButton();

        // Verify that the deleted value of Leave Type is not in the data table
        var deletedData = _myLeaveBalanceReportPage
            .GetDataColumn(0, false)
            .FirstOrDefault(item => item.Text == deletedLeaveTypeName);
        Assert.IsNull(deletedData);

        // Recover the deleted leave type
        DeleteLeaveTypePostcondition(deletedLeaveTypeName);
    }

    [TestMethod(
        "TC_DATA_SECTION_07 - Verify that the new value of Leave Type shows in the data table."
    )]
    [TestCategory("TC_DATA_SECTION")]
    public void TestSelectReportTypeUnsuccessfully()
    {
        // Add a new leave type
        var newLeaveTypeName = AddLeaveTypePrecondition();

        // Generate a random report
        _myLeaveBalanceReportPage.ClickDropdown();
        _myLeaveBalanceReportPage.SelectDropdownOption(1);
        _myLeaveBalanceReportPage.ClickGenerateButton();

        // Verify that the deleted value of Leave Type is not in the data table
        var newData = _myLeaveBalanceReportPage
            .GetDataColumn(0, false)
            .FirstOrDefault(item => item.Text == newLeaveTypeName);
        Assert.IsNotNull(newData);

        AddLeaveTypePostcondition(newLeaveTypeName);
    }
}
