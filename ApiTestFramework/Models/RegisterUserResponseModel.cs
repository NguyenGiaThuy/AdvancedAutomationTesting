namespace ApiTestFramework.Models;

public record RegisterUserResponseModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("token")]
    public string Token { get; set; } = default!;
}
