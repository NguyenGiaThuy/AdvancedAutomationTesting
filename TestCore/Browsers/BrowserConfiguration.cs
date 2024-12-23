namespace TestCore.Browsers;

public class BrowserConfiguration : IConfiguration
{
    private readonly IConfigurationRoot _configuration;

    public BrowserConfiguration(string configFilePath)
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(configFilePath, optional: false, reloadOnChange: true)
            .Build();
    }

    public string? this[string key]
    {
        get => _configuration[key];
        set => throw new NotSupportedException("Setting values directly is not supported.");
    }

    public IEnumerable<IConfigurationSection> GetChildren()
    {
        return _configuration.GetChildren();
    }

    public IChangeToken GetReloadToken()
    {
        return _configuration.GetReloadToken();
    }

    public IConfigurationSection GetSection(string key)
    {
        return _configuration.GetSection(key);
    }
}
