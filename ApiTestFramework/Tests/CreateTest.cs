namespace ApiTestFramework.Tests;

[TestClass]
public class CreateTest : TestBase
{
    [TestMethod("TC_CREATE_01 - Verify that the POST method can create a new user.")]
    [TestCategory("TC_CREATE")]
    [DataRow(["testuser1", "sdet"], DisplayName = "name = testuser1, job = sdet")]
    [DataRow(["testuser2", "dev"], DisplayName = "name = testuser2, job = dev")]
    public async Task TestCreateNewUserSuccessfully(string[] parameters)
    {
        var requestBody = new CreateUserRequestModel { Name = parameters[0], Job = parameters[1] };

        var response = await _client.Post<CreateUserRequestModel, CreateUserResponseModel>(
            $"/api/users",
            requestBody,
            CancellationToken.None
        );

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod(
        "TC_CREATE_02 - Verify that the POST method cannot create a new user with invalid details."
    )]
    [TestCategory("TC_CREATE")]
    public async Task TestCreateInvalidUserUnsuccessfully()
    {
        // Use response model to create a user with invalid details
        var requestBody = new CreateUserResponseModel
        {
            Name = "testuser1",
            Job = "sdet",
            Id = 1,
            CreatedAt = "2021-09-01T00:00:00.000Z",
        };

        var response = await _client.Post<CreateUserResponseModel, CreateUserResponseModel>(
            $"/api/users",
            requestBody,
            CancellationToken.None
        );

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
