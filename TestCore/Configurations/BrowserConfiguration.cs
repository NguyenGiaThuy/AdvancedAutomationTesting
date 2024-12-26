namespace TestCore.Browsers;

public class BrowserConfiguration
{
    private readonly IConfigurationRoot _configuration;

    public BrowserConfiguration(string defaultConfigFilePath, string environmentConfigFilePath)
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

    public int? GetImplicitTimeout()
    {
        return int.TryParse(_configuration["ImplicitTimeout"], out var result) ? result : null;
    }

    public string? GetBrowserOptions()
    {
        return _configuration["BrowserOptions"];
    }

    public BrowserType? GetBrowserType()
    {
        return Enum.TryParse<BrowserType>(_configuration["Browser"], out var result)
            ? result
            : null;
        ;
    }
}
