namespace TestCore.Apis;

public interface IApiClient : IDisposable
{
    public Task<Response<TResponseData>> Get<TResponseData>(
        string resource,
        CancellationToken cancellationToken
    );

    public Task<Response<TResponseData>> Post<TRequestData, TResponseData>(
        string resource,
        TRequestData data,
        CancellationToken cancellationToken
    );

    public Task<Response<TResponseData>> Put<TRequestData, TResponseData>(
        string resource,
        TRequestData data,
        CancellationToken cancellationToken
    );

    public Task<Response<TResponseData>> Patch<TRequestData, TResponseData>(
        string resource,
        TRequestData data,
        CancellationToken cancellationToken
    );

    public Task<Response<TResponseData>> Delete<TResponseData>(
        string resource,
        CancellationToken cancellationToken
    );
}
