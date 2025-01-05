namespace ApiTestFramework.Tests;

[TestClass]
public class GetTest : TestBase
{
    [TestMethod("TC_GET_01 - Verify that the GET method can get all users list.")]
    [TestCategory("TC_GET")]
    [TestCategory("API")]
    [DataRow("?page=1&per_page=1", DisplayName = "page = 1, per_page = 1")]
    [DataRow("?page=12&per_page=1", DisplayName = "page = 12, per_page = 1")]
    [DataRow("?page=1&per_page=12", DisplayName = "page = 1, per_page = 12")]
    public async Task TestGetUsersListSuccessfully(string parameters)
    {
        _reportHelper.LogMessage(
            Status.Info,
            $"Getting users list with query string: {parameters}"
        );

        var response = await _client.Get<GetUsersListModel>(
            $"/api/users{parameters}",
            CancellationToken.None
        );

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod("TC_GET_02 - Verify that the GET method can get a user with valid id.")]
    [TestCategory("TC_GET")]
    [TestCategory("API")]
    [DataRow("1", DisplayName = "id = 1")]
    [DataRow("2", DisplayName = "id = 2")]
    public async Task TestGetValidUserSuccessfully(string id)
    {
        _reportHelper.LogMessage(Status.Info, $"Getting a valid user with id: {id}");

        var response = await _client.Get<GetUserModel>($"/api/user/{id}", CancellationToken.None);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod("TC_GET_03 - Verify that the GET method cannot get a user with invalid id.")]
    [TestCategory("TC_GET")]
    [TestCategory("API")]
    [DataRow("-1", DisplayName = "id = -1")]
    [DataRow("a", DisplayName = "id = a")]
    [DataRow("", DisplayName = "id = ''")]
    public async Task TestGetInvalidUserUnsuccessfully(string id)
    {
        _reportHelper.LogMessage(Status.Info, $"Getting an invalid user with id: {id}");

        var response = await _client.Get<GetUserModel>($"/api/user/{id}", CancellationToken.None);

        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
