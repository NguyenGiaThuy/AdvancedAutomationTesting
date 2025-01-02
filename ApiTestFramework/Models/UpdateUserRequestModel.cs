namespace ApiTestFramework.Models;

public record UpdateUserRequestModel
{
    [JsonProperty("name")]
    public string Name { get; set; } = default!;

    [JsonProperty("job")]
    public string Job { get; set; } = default!;
}
