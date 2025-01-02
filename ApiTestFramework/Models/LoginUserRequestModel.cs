namespace ApiTestFramework.Models;

public record LoginUserRequestModel
{
    [JsonProperty("email")]
    public string Email { get; set; } = default!;

    [JsonProperty("password")]
    public string Password { get; set; } = default!;
}
