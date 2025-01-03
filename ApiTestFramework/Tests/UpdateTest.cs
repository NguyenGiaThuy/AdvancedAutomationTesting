namespace ApiTestFramework.Tests;

[TestClass]
public class UpdateTest : TestBase
{
    [TestMethod("TC_UPDATE_01 - Verify that the PUT method can update a user.")]
    [TestCategory("TC_UPDATE")]
    [TestCategory("API")]
    [DataRow(["1", "testuser1", "sdet"], DisplayName = "id = 1, name = testuser1, job = sdet")]
    [DataRow(["2", "testuser2", "dev"], DisplayName = "id = 2, name = testuser2, job = dev")]
    public async Task TestPutUpdateUserSuccessfully(string[] parameters)
    {
        var requestBody = new UpdateUserRequestModel { Name = parameters[1], Job = parameters[2] };

        var response = await _client.Put<UpdateUserRequestModel, UpdateUserResponseModel>(
            $"/api/users/{parameters[0]}",
            requestBody,
            CancellationToken.None
        );

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod("TC_UPDATE_02 - Verify that the PUT method cannot update an invalid user.")]
    [TestCategory("TC_UPDATE")]
    [TestCategory("API")]
    [DataRow(["-1", "testuser1", "sdet"], DisplayName = "id = -1, name = testuser1, job = sdet")]
    [DataRow(["a", "testuser2", "dev"], DisplayName = "id = a, name = testuser2, job = dev")]
    public async Task TestPutUpdateUserUnsuccessfully(string[] parameters)
    {
        var requestBody = new UpdateUserRequestModel { Name = parameters[1], Job = parameters[2] };

        var response = await _client.Put<UpdateUserRequestModel, UpdateUserResponseModel>(
            $"/api/users/{parameters[0]}",
            requestBody,
            CancellationToken.None
        );

        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [TestMethod("TC_UPDATE_03 - Verify that the PATCH method can update a user.")]
    [TestCategory("TC_UPDATE")]
    [TestCategory("API")]
    [DataRow(["1", "testuser1", "sdet"], DisplayName = "id = 1, name = testuser1, job = sdet")]
    [DataRow(["2", "testuser2", "dev"], DisplayName = "id = 2, name = testuser2, job = dev")]
    public async Task TestPatchUpdateUserSuccessfully(string[] parameters)
    {
        var requestBody = new UpdateUserRequestModel { Name = parameters[1], Job = parameters[2] };

        var response = await _client.Patch<UpdateUserRequestModel, UpdateUserResponseModel>(
            $"/api/users/{parameters[0]}",
            requestBody,
            CancellationToken.None
        );

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod("TC_UPDATE_04 - Verify that the PATCH method cannot update an invalid user.")]
    [TestCategory("TC_UPDATE")]
    [TestCategory("API")]
    [DataRow(["-1", "testuser1", "sdet"], DisplayName = "id = -1, name = testuser1, job = sdet")]
    [DataRow(["a", "testuser2", "dev"], DisplayName = "id = a, name = testuser2, job = dev")]
    public async Task TestPatchUpdateUserUnsuccessfully(string[] parameters)
    {
        var requestBody = new UpdateUserRequestModel { Name = parameters[1], Job = parameters[2] };

        var response = await _client.Patch<UpdateUserRequestModel, UpdateUserResponseModel>(
            $"/api/users/{parameters[0]}",
            requestBody,
            CancellationToken.None
        );

        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
