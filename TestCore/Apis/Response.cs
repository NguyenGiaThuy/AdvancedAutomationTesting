namespace TestCore.Apis;

public class Response<TResponseData>(HttpStatusCode statusCode, TResponseData? data)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public TResponseData? Data { get; } = data;
}
