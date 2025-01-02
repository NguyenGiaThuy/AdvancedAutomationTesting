namespace TestCore.Apis;

public static class ApiFactory
{
    public static IApiClient MakeApi(string baseUrl)
    {
        return new ApiClient(baseUrl);
    }
}
