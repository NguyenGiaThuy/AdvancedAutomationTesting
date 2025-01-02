namespace ApiTestFramework.Models;

public record LoginUserResponseModel
{
    [JsonProperty("token")]
    public string Token { get; set; } = default!;
}
