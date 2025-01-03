namespace TestCore.Apis;

public interface IApiClient : IDisposable
{
    /// <summary>
    /// Sends a GET request to the specified resource.
    /// </summary>
    /// <typeparam name="TResponseData"></typeparam>
    /// <param name="resource">
    /// The resource to send the request to.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The response from the GET request.
    /// </returns>
    public Task<Response<TResponseData>> Get<TResponseData>(
        string resource,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Sends a POST request to the specified resource.
    /// </summary>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TResponseData"></typeparam>
    /// <param name="resource">
    /// The resource to send the request to.
    /// </param>
    /// <param name="data">
    /// The data to send in the request body.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The response from the POST request.
    /// </returns>
    public Task<Response<TResponseData>> Post<TRequestData, TResponseData>(
        string resource,
        TRequestData data,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Sends a PUT request to the specified resource.
    /// </summary>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TResponseData"></typeparam>
    /// <param name="resource">
    /// The resource to send the request to.
    /// </param>
    /// <param name="data">
    /// The data to send in the request body.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The response from the PUT request.
    /// </returns>
    public Task<Response<TResponseData>> Put<TRequestData, TResponseData>(
        string resource,
        TRequestData data,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Sends a PATCH request to the specified resource.
    /// </summary>
    /// <typeparam name="TRequestData"></typeparam>
    /// <typeparam name="TResponseData"></typeparam>
    /// <param name="resource">
    /// The resource to send the request to.
    /// </param>
    /// <param name="data">
    /// The data to send in the request body.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The response from the PATCH request.
    /// </returns>
    public Task<Response<TResponseData>> Patch<TRequestData, TResponseData>(
        string resource,
        TRequestData data,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Sends a DELETE request to the specified resource.
    /// </summary>
    /// <typeparam name="TResponseData"></typeparam>
    /// <param name="resource">
    /// The resource to send the request to.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token.
    /// </param>
    /// <returns>
    /// The response from the DELETE request.
    /// </returns>
    public Task<Response<TResponseData>> Delete<TResponseData>(
        string resource,
        CancellationToken cancellationToken
    );
}
