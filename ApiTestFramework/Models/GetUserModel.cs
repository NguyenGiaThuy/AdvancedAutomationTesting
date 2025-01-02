namespace ApiTestFramework.Models;

public record GetUserModel
{
    [JsonProperty("data")]
    public User UserData { get; set; } = default!;

    [JsonProperty("support")]
    public Support SupportData { get; set; } = default!;

    public record User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = default!;

        [JsonProperty("year")]
        public int Year { get; set; } = default!;

        [JsonProperty("color")]
        public string Color { get; set; } = default!;

        [JsonProperty("pantone_value")]
        public string PantoneValue { get; set; } = default!;
    }

    public record Support
    {
        [JsonProperty("url")]
        public string Url { get; set; } = default!;

        [JsonProperty("text")]
        public string Text { get; set; } = default!;
    }
}
