namespace ApiTestFramework.Tests;

[TestClass]
public class DeleteTest : TestBase
{
    [TestMethod("TC_DELETE_01 - Verify that the DELETE method can delete a user.")]
    [TestCategory("TC_DELETE")]
    [TestCategory("API")]
    [DataRow("1", DisplayName = "id = 1")]
    [DataRow("2", DisplayName = "id = 2")]
    public async Task TestDeleteUserSuccessfully(string id)
    {
        _reportHelper.LogMessage(Status.Info, $"Deleting a valid user with id: {id}");

        var response = await _client.Delete<string?>($"/api/users/{id}", CancellationToken.None);

        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        Assert.IsNull(response.Data);
    }

    [TestMethod("TC_DELETE_02 - Verify that the DELETE method cannot delete an invalid user.")]
    [TestCategory("TC_DELETE")]
    [TestCategory("API")]
    [DataRow("-1", DisplayName = "id = -1")]
    [DataRow("a", DisplayName = "id = a")]
    [DataRow("", DisplayName = "id = ''")]
    public async Task TestDeleteInvalidUserUnsuccessfully(string id)
    {
        _reportHelper.LogMessage(Status.Info, $"Deleting an invalid user with id: {id}");

        var response = await _client.Delete<string?>($"/api/users/{id}", CancellationToken.None);

        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
