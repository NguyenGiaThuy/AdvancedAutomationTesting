namespace TestCore.Apis;

public class ApiClient : IApiClient
{
    RestClient _client = null!;

    public ApiClient(string baseUrl)
    {
        _client = new RestClient(new RestClientOptions(baseUrl));
    }

    public async Task<Response<TResponseData>> Get<TResponseData>(
        string resource,
        CancellationToken cancellationToken
    )
    {
        var request = new RestRequest(resource, Method.Get);
        var response = await _client.ExecuteAsync(request, cancellationToken);

        var statusCode = response.StatusCode;
        TResponseData? responseData;
        try
        {
            responseData = JsonConvert.DeserializeObject<TResponseData>(response.Content!);
        }
        catch
        {
            responseData = default;
        }

        var responseObj = new Response<TResponseData>(statusCode, responseData);

        return responseObj!;
    }

    public async Task<Response<TResponseData>> Post<TRequestData, TResponseData>(
        string resource,
        TRequestData data,
        CancellationToken cancellationToken
    )
    {
        var request = new RestRequest(resource, Method.Post);
        request.AddJsonBody(JsonConvert.SerializeObject(data));
        var response = await _client.ExecuteAsync(request, cancellationToken);

        var statusCode = response.StatusCode;
        TResponseData? responseData;
        try
        {
            responseData = JsonConvert.DeserializeObject<TResponseData>(response.Content!);
        }
        catch
        {
            responseData = default;
        }

        var responseObj = new Response<TResponseData>(statusCode, responseData);

        return responseObj!;
    }

    public async Task<Response<TResponseData>> Put<TRequestData, TResponseData>(
        string resource,
        TRequestData data,
        CancellationToken cancellationToken
    )
    {
        var request = new RestRequest(resource, Method.Put);
        request.AddJsonBody(JsonConvert.SerializeObject(data));
        var response = await _client.ExecuteAsync(request, cancellationToken);

        var statusCode = response.StatusCode;
        TResponseData? responseData;
        try
        {
            responseData = JsonConvert.DeserializeObject<TResponseData>(response.Content!);
        }
        catch
        {
            responseData = default;
        }

        var responseObj = new Response<TResponseData>(statusCode, responseData);

        return responseObj!;
    }

    public async Task<Response<TResponseData>> Patch<TRequestData, TResponseData>(
        string resource,
        TRequestData data,
        CancellationToken cancellationToken
    )
    {
        var request = new RestRequest(resource, Method.Patch);
        request.AddJsonBody(JsonConvert.SerializeObject(data));
        var response = await _client.ExecuteAsync(request, cancellationToken);

        var statusCode = response.StatusCode;
        TResponseData? responseData;
        try
        {
            responseData = JsonConvert.DeserializeObject<TResponseData>(response.Content!);
        }
        catch
        {
            responseData = default;
        }

        var responseObj = new Response<TResponseData>(statusCode, responseData);

        return responseObj!;
    }

    public async Task<Response<TResponseData>> Delete<TResponseData>(
        string resource,
        CancellationToken cancellationToken
    )
    {
        var request = new RestRequest(resource, Method.Delete);
        var response = await _client.ExecuteAsync(request, cancellationToken);

        var statusCode = response.StatusCode;
        TResponseData? responseData = default;
        var responseObj = new Response<TResponseData>(statusCode, responseData);

        return responseObj!;
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
