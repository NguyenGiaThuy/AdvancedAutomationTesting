namespace TestCore.Configurations;

public class ApiConfiguration(string defaultConfigFilePath, string environmentConfigFilePath)
    : ConfigurationBase(defaultConfigFilePath, environmentConfigFilePath)
{
    public string? GetReportDir()
    {
        return _configuration["ReportDir"];
    }

    public string? GetReportFile()
    {
        return _configuration["ReportFile"];
    }
}
