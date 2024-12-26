namespace TestCore.Helpers;

public class EnvironmentHelperBase
{
    public string? GetVariable(string variableName)
    {
        return Environment.GetEnvironmentVariable(variableName);
    }

    public string? GetTestEnvironment()
    {
        return Environment.GetEnvironmentVariable("TEST_ENVIRONMENT");
    }
}
