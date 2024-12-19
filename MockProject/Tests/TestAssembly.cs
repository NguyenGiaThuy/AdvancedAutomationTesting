namespace MockProject.Tests;

[TestClass]
public class TestAssembly
{
    public static IConfiguration Configuration = null!;

    [AssemblyInitialize]
    public static void AssemblyPrecondition(TestContext testContext)
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
}
