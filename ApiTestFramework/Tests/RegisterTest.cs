namespace ApiTestFramework.Tests;

[TestClass]
public class RegisterTest : TestBase
{
    [TestMethod("TC_REGISTER_01 - Verify that the POST method can register a new user.")]
    [TestCategory("TC_REGISTER")]
    [TestCategory("API")]
    [DataRow(
        ["janet.weaver@reqres.in", "testpassword1"],
        DisplayName = "email = janet.weaver@reqres.in, password = testpassword1"
    )]
    [DataRow(
        ["eve.holt@reqres.in", "testpassword2"],
        DisplayName = "email = eve.holt@reqres.in, password = testpassword2"
    )]
    public async Task TestRegisterNewUserSuccessfully(string[] parameters)
    {
        var email = parameters[0];
        var password = parameters[1];
        ReportHelper.LogMessage(
            _test,
            Status.Info,
            $"Registering with email: {email}\nRegistering with password: {password}"
        );

        var requestBody = new RegisterUserRequestModel { Email = email, Password = password };

        var response = await _client.Post<RegisterUserRequestModel, RegisterUserResponseModel>(
            $"/api/register",
            requestBody,
            CancellationToken.None
        );

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod(
        "TC_REGISTER_02 - Verify that the POST method cannot register a new user with invalid details."
    )]
    [TestCategory("TC_REGISTER")]
    [TestCategory("API")]
    [DataRow(["eve.holt@reqres.in", ""], DisplayName = "email = eve.holt@reqres.in, password = ''")]
    [DataRow(["", "testpassword2"], DisplayName = "email = '', password = testpassword2")]
    public async Task TestRegisterInvalidUserUnsuccessfully(string[] parameters)
    {
        var email = parameters[0];
        var password = parameters[1];
        ReportHelper.LogMessage(
            _test,
            Status.Info,
            $"Registering with email: {email}\nRegistering with password: {password}"
        );

        var requestBody = new RegisterUserRequestModel { Email = email, Password = password };

        var response = await _client.Post<RegisterUserRequestModel, RegisterUserResponseModel>(
            $"/api/register",
            requestBody,
            CancellationToken.None
        );

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
