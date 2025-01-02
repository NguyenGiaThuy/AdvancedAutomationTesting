namespace TestCore.Configurations;

public class ConfigurationBase
{
    protected readonly IConfigurationRoot _configuration;

    public ConfigurationBase(string defaultConfigFilePath, string environmentConfigFilePath)
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(defaultConfigFilePath, optional: false, reloadOnChange: true)
            .AddJsonFile(environmentConfigFilePath, optional: true)
            .Build();
    }

    public string? GetBaseUrl()
    {
        return _configuration["BaseUrl"];
    }
}
