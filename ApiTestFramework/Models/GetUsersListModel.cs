namespace ApiTestFramework.Models;

public record GetUsersListModel
{
    [JsonProperty("page")]
    public int Page { get; set; }

    [JsonProperty("per_page")]
    public int PerPage { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("total_pages")]
    public int TotalPages { get; set; }

    [JsonProperty("data")]
    public List<User> UsersData { get; set; } = [];

    [JsonProperty("support")]
    public Support SupportData { get; set; } = default!;

    public record User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; } = default!;

        [JsonProperty("first_name")]
        public string FirstName { get; set; } = default!;

        [JsonProperty("last_name")]
        public string LastName { get; set; } = default!;

        [JsonProperty("avatar")]
        public string Avatar { get; set; } = default!;
    }

    public record Support
    {
        [JsonProperty("url")]
        public string Url { get; set; } = default!;

        [JsonProperty("text")]
        public string Text { get; set; } = default!;
    }
}
