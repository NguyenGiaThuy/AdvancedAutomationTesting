namespace TestCore.Helpers;

public class BrowserEnvironmentHelper : EnvironmentHelperBase
{
    public BrowserType? GetBrowserType()
    {
        return Enum.TryParse<BrowserType>(
            Environment.GetEnvironmentVariable("BROWSER"),
            out var result
        )
            ? result
            : null;
        ;
    }

    public int? GetImplicitTimeout()
    {
        return Environment.GetEnvironmentVariable("IMPLICIT_TIMEOUT") != null
            ? int.Parse(Environment.GetEnvironmentVariable("IMPLICIT_TIMEOUT")!)
            : null;
    }

    public string? GetBrowserOptions()
    {
        return Environment.GetEnvironmentVariable("BROWSER_OPTIONS");
    }
}
