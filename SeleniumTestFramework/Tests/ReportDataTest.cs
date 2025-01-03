namespace SeleniumTestFramework.Tests;

[TestClass]
public class ReportDataTest : TestBase
{
    private MyLeaveBalanceReportPage _myLeaveBalanceReportPage = null!;

    [TestInitialize]
    public override void TestPrecondition()
    {
        base.TestPrecondition();

        try
        {
            // Navigate to the My Leave Entitlements and Usage Report page
            _myLeaveBalanceReportPage = new MyLeaveBalanceReportPage(_browser, _baseUrl);
            _myLeaveBalanceReportPage.GoToPage();
            _testContext.WriteLine(
                "Successfully navigated to the My Leave Entitlements and Usage Report page"
            );
        }
        catch (WebException ex)
        {
            _testContext.WriteLine("Failed to load page due to connection");
            throw new PreconditionException("Failed to load page due to connection", ex);
        }
    }

    [TestMethod("TC_DATA_SECTION_01 - Verify that the data table can be enlarged.")]
    [TestCategory("TC_DATA_SECTION")]
    [TestCategory("SELENIUM")]
    public void TestEnlargeDataTableSuccessfully()
    {
        // Click the Enlarge button
        _myLeaveBalanceReportPage.ClickEnlargeButton();

        // Expected result: The Enlarge button is not visible, which means the data table is enlarged
        var visible = _myLeaveBalanceReportPage.EnlargeButtonIsVisible();
        Assert.IsFalse(visible);
    }

    [TestMethod("TC_DATA_SECTION_02 - Verify that the data table can be shrunk.")]
    [TestCategory("TC_DATA_SECTION")]
    [TestCategory("SELENIUM")]
    public void TestShrinkDataTableSuccessfully()
    {
        /* Precondition: The data table should have been enlarged */
        // Click the Enlarge button
        _myLeaveBalanceReportPage.ClickEnlargeButton();

        /* Test step(s) */
        // Click the Shrink button
        _myLeaveBalanceReportPage.ClickShrinkButton();

        // Expected result: The Shrink button is not visible, which means the data table is shrunk
        var visible = _myLeaveBalanceReportPage.ShrinkButtonIsVisible();
        Assert.IsFalse(visible);
    }

    [TestMethod(
        "TC_DATA_SECTION_04 - Verify that the report counter matches the total number of records generated."
    )]
    [TestCategory("TC_DATA_SECTION")]
    [TestCategory("SELENIUM")]
    public void TestReportCounterMatchesNumOfRows()
    {
        /* Precondition: The data table should have been generated */
        // Generate a random report
        _myLeaveBalanceReportPage.ClickDropdown();
        _myLeaveBalanceReportPage.SelectDropdownOption(1);
        _myLeaveBalanceReportPage.ClickGenerateButton();

        /* Test step(s) */
        // Step 1: Get the report counter
        var counter = _myLeaveBalanceReportPage.GetRecordsCounter();

        // Step 2: Get the number of rows in the data table
        var nRows = _myLeaveBalanceReportPage.GetDataColumn(0, false).Count();

        // Expected result: The report counter matches the number of rows, which means the report counter is correct
        Assert.AreEqual(nRows, counter);
    }

    [TestMethod("TC_DATA_SECTION_05 - Verify that the data types in the data table are valid.")]
    [TestCategory("TC_DATA_SECTION")]
    [TestCategory("SELENIUM")]
    public void TestReportDataTypesCorrect()
    {
        /* Precondition: The data table should have been generated */
        // Generate a random report
        _myLeaveBalanceReportPage.ClickDropdown();
        _myLeaveBalanceReportPage.SelectDropdownOption(1);
        _myLeaveBalanceReportPage.ClickGenerateButton();

        /* Test step(s) */
        // Navigate each data cell in the data table
        var firstColumn = _myLeaveBalanceReportPage.GetDataColumn(0, false);
        foreach (var item in firstColumn)
        {
            // Expected result: All data cell in the first column is of type string
            Assert.IsInstanceOfType(item.Text, typeof(string));
        }

        for (var i = 1; i < 6; i++)
        {
            var column = _myLeaveBalanceReportPage.GetDataColumn(i, false);

            foreach (var item in column)
            {
                // Expected result: All data cell in the first column is of type decimal
                Assert.IsInstanceOfType(decimal.Parse(item.Text), typeof(decimal));
            }
        }
    }

    [TestMethod(
        "TC_DATA_SECTION_06 - Verify that the deleted value of Leave Type does not show in the data table."
    )]
    [TestCategory("TC_DATA_SECTION")]
    [TestCategory("SELENIUM")]
    public void TestDeletedLeaveTypeDoesnotShowSuccessfully()
    {
        /* Precondition: A random value in Leave Types should have been deleted
        and report should have been generated */
        // Delete the leave type
        var deletedLeaveTypeName = DeleteLeaveTypePrecondition();

        // Generate a random report
        _myLeaveBalanceReportPage.ClickDropdown();
        _myLeaveBalanceReportPage.SelectDropdownOption(1);
        _myLeaveBalanceReportPage.ClickGenerateButton();

        // Test step(s)
        // Try getting the data cell with the deleted value of Leave Type
        var deletedData = _myLeaveBalanceReportPage
            .GetDataColumn(0, false)
            .FirstOrDefault(item => item.Text == deletedLeaveTypeName);

        // Expected result: The deleted value of Leave Type is not in the data table
        Assert.IsNull(deletedData);

        /* Postcondition: Recover the deleted leave type in the Leave Types */
        DeleteLeaveTypePostcondition(deletedLeaveTypeName);
    }

    [TestMethod(
        "TC_DATA_SECTION_07 - Verify that the new value of Leave Type shows in the data table."
    )]
    [TestCategory("TC_DATA_SECTION")]
    [TestCategory("SELENIUM")]
    public void TestSelectReportTypeUnsuccessfully()
    {
        /* Precondition: A new random value in Leave Types should have been added
        and report should have been generated */
        // Add a new leave type
        var newLeaveTypeName = AddLeaveTypePrecondition();

        // Generate a random report
        _myLeaveBalanceReportPage.ClickDropdown();
        _myLeaveBalanceReportPage.SelectDropdownOption(1);
        _myLeaveBalanceReportPage.ClickGenerateButton();

        /* Test step(s) */
        // Try getting the data cell with the newly added value of Leave Type
        var newData = _myLeaveBalanceReportPage
            .GetDataColumn(0, false)
            .FirstOrDefault(item => item.Text == newLeaveTypeName);

        // Expected result: The newly added value of Leave Type is in the data table
        Assert.IsNotNull(newData);

        /* Postcondition: Delete newly added leave type in the Leave Types */
        AddLeaveTypePostcondition(newLeaveTypeName);
    }

    private string DeleteLeaveTypePrecondition()
    {
        try
        {
            // Navigate to the Leave Types page
            var leaveTypesPage = new LeaveTypesPage(_browser, _baseUrl);
            leaveTypesPage.GoToPage();
            _testContext.WriteLine("Successfully navigated to the Leave Types page");

            // Get the first Leave Type name
            var leaveTypeName = leaveTypesPage.GetLeaveTypeNameByIndex(0);

            // Delete the first Leave Type
            leaveTypesPage.ClickDeleteLeaveTypeButtonByIndex(0);
            leaveTypesPage.ClickConfirmDeleteButton();
            _testContext.WriteLine($"Successfully deleted the leave type: {leaveTypeName}");

            // Navigate to the My Leave Entitlements and Usage Report page
            _myLeaveBalanceReportPage = new MyLeaveBalanceReportPage(_browser, _baseUrl);
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
            var leaveTypesPage = new LeaveTypesPage(_browser, _baseUrl);
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
            var leaveTypesPage = new LeaveTypesPage(_browser, _baseUrl);
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
            _myLeaveBalanceReportPage = new MyLeaveBalanceReportPage(_browser, _baseUrl);
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
            var leaveTypesPage = new LeaveTypesPage(_browser, _baseUrl);
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
}
