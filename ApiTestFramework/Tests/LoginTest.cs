namespace ApiTestFramework.Tests;

[TestClass]
public class LoginTest : TestBase
{
    [TestMethod("TC_LOGIN_01 - Verify that the POST method can login a user.")]
    [TestCategory("TC_LOGIN")]
    [TestCategory("API")]
    [DataRow(
        ["janet.weaver@reqres.in", "testpassword1"],
        DisplayName = "email = janet.weaver@reqres.in, password = testpassword1"
    )]
    [DataRow(
        ["eve.holt@reqres.in", "testpassword2"],
        DisplayName = "email = eve.holt@reqres.in, password = testpassword2"
    )]
    public async Task TestLoginUserSuccessfully(string[] parameters)
    {
        var requestBody = new LoginUserRequestModel
        {
            Email = parameters[0],
            Password = parameters[1],
        };

        var response = await _client.Post<LoginUserRequestModel, LoginUserResponseModel>(
            $"/api/login",
            requestBody,
            CancellationToken.None
        );

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod(
        "TC_LOGIN_02 - Verify that the POST method cannot login a user with invalid details."
    )]
    [TestCategory("TC_LOGIN")]
    [TestCategory("API")]
    [DataRow(["eve.holt@reqres.in", ""], DisplayName = "email = eve.holt@reqres.in, password = ''")]
    [DataRow(["", "testpassword2"], DisplayName = "email = '', password = testpassword2")]
    public async Task TestLoginInvalidUserUnsuccessfully(string[] parameters)
    {
        var requestBody = new LoginUserRequestModel
        {
            Email = parameters[0],
            Password = parameters[1],
        };

        var response = await _client.Post<LoginUserRequestModel, LoginUserResponseModel>(
            $"/api/register",
            requestBody,
            CancellationToken.None
        );

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
