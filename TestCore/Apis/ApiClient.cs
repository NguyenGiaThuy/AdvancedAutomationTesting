namespace TestCore.Apis;

public class ApiClient : IApiClient
{
    RestClient _client = null!;

    public ApiClient(string baseUrl)
    {
        _client = new RestClient(new RestClientOptions(baseUrl));
    }

    public async Task<Response<TData>> Get<TData>(
        string resource,
        CancellationToken cancellationToken
    )
    {
        var request = new RestRequest(resource, Method.Get);
        var response = await _client.ExecuteAsync(request, cancellationToken);

        var statusCode = response.StatusCode;
        TData? responseData;
        try
        {
            responseData = JsonConvert.DeserializeObject<TData>(response.Content!);
        }
        catch
        {
            responseData = default(TData);
        }

        var responseObj = new Response<TData>(statusCode, responseData);

        return responseObj!;
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
