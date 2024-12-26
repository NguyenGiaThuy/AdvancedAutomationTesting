namespace TestCore.Browsers;

public class ApiConfiguration
{
    private readonly IConfigurationRoot _configuration;

    public ApiConfiguration(string defaultConfigFilePath, string environmentConfigFilePath)
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(defaultConfigFilePath, optional: false, reloadOnChange: true)
            .AddJsonFile(environmentConfigFilePath, optional: true)
            .Build();
    }
}
