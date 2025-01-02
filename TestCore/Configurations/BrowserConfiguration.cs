namespace TestCore.Configurations;

public class BrowserConfiguration(string defaultConfigFilePath, string environmentConfigFilePath)
    : ConfigurationBase(defaultConfigFilePath, environmentConfigFilePath)
{
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
