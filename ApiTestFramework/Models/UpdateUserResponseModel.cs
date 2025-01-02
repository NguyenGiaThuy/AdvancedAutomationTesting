namespace ApiTestFramework.Models;

public record UpdateUserResponseModel
{
    [JsonProperty("name")]
    public string Name { get; set; } = default!;

    [JsonProperty("job")]
    public string Job { get; set; } = default!;

    [JsonProperty("updatedAt")]
    public string UpdatedAt { get; set; } = default!;
}
