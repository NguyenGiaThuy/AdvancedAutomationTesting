namespace ApiTestFramework.Models;

public record CreateUserResponseModel
{
    [JsonProperty("name")]
    public string Name { get; set; } = default!;

    [JsonProperty("job")]
    public string Job { get; set; } = default!;

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("createdAt")]
    public string CreatedAt { get; set; } = default!;
}
