namespace TestCore.Helpers;

public class ReportHelper
{
    private ExtentReports _extent = default!;
    private ExtentTest _test = default!;

    public void InitializeReport(string reportDir, string reportFile)
    {
        _extent = new ExtentReports();

        if (!Directory.Exists(Path.Combine(AppContext.BaseDirectory, reportDir)))
        {
            Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, reportDir));
        }
        var reportPath = Path.Combine(
            Path.Combine(AppContext.BaseDirectory, reportDir),
            $"{reportFile}-{DateTime.Now.ToFileTimeUtc()}.html"
        );
        var spark = new ExtentSparkReporter(reportPath);

        _extent.AttachReporter(spark);
    }

    public void CreateTestCase(string testCaseTilte, string testCaseDescription)
    {
        _test = _extent.CreateTest(testCaseTilte, testCaseDescription);
    }

    public void LogMessage(Status status, string detail)
    {
        switch (status)
        {
            case Status.Pass:
                _test.Pass(detail);
                break;
            case Status.Fail:
                _test.Fail(detail);
                break;
            case Status.Warning:
                _test.Warning(detail);
                break;
            case Status.Info:
                _test.Info(detail);
                break;
            default:
                _test.Info(detail);
                break;
        }
    }

    public void ExportReport()
    {
        _extent.Flush();
    }
}
