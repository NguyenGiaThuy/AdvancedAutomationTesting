namespace ApiTestFramework.Models;

public record CreateUserRequestModel
{
    [JsonProperty("name")]
    public string Name { get; set; } = default!;

    [JsonProperty("job")]
    public string Job { get; set; } = default!;
}
