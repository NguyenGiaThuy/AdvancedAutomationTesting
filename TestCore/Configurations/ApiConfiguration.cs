namespace TestCore.Configurations;

public class ApiConfiguration(string defaultConfigFilePath, string environmentConfigFilePath)
    : ConfigurationBase(defaultConfigFilePath, environmentConfigFilePath) { }
