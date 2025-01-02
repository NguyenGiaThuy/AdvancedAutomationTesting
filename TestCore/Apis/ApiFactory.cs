namespace TestCore.Apis;

public static class ApiFactory
{
    public static IApiClient MakeApiClient(string baseUrl)
    {
        return new ApiClient(baseUrl);
    }
}
