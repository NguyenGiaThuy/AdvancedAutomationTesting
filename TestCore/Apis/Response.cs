namespace TestCore.Apis;

public class Response<TData>(HttpStatusCode statusCode, TData? data)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public TData? Data { get; } = data;
}
