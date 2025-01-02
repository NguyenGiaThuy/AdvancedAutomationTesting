namespace TestCore.Apis;

public interface IApiClient : IDisposable
{
    /// <summary>
    /// Sends a GET request to the specified API resource and retrieves a response of type <typeparamref name="TData"/>.
    /// </summary>
    /// <param name="resource">The relative or absolute URI of the API resource to request.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a <see cref="Response{TData}"/>
    /// object with the response data.
    /// </returns>
    public Task<Response<TData>> Get<TData>(string resource, CancellationToken cancellationToken);
}
