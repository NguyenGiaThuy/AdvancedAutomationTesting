namespace TestCore.Helpers;

public static class ReportHelper
{
    private static ExtentReports _extent = default!;

    public static void InitializeReport(string reportDir, string reportFile)
    {
        if (_extent != null)
        {
            return;
        }

        var reportPath = Path.Combine(
            Path.Combine(AppContext.BaseDirectory, reportDir),
            $"{reportFile}_{DateTime.UtcNow:dd-MM-yyyy}.html"
        );

        if (!Directory.Exists(reportDir))
        {
            Directory.CreateDirectory(reportDir);
        }

        var spark = new ExtentSparkReporter(reportPath);
        _extent = new ExtentReports();
        _extent.AttachReporter(spark);
    }

    public static ExtentTest CreateTest(string testCaseTitle, string testCaseDescription)
    {
        return _extent.CreateTest(testCaseTitle, testCaseDescription);
    }

    public static void LogMessage(ExtentTest test, Status status, string detail)
    {
        test.Log(status, detail);
    }

    public static void GenerateReport()
    {
        _extent.Flush();
    }
}
