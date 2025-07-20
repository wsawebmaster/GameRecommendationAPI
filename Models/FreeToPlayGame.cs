using System.Text.Json.Serialization;
namespace GameRecommendationAPI.Models;

public class FreeToPlayGame
{
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("genre")]
    public string Genre { get; set; } = string.Empty;

    [JsonPropertyName("platform")]
    public string Platform { get; set; } = string.Empty;

    [JsonPropertyName("game_url")]
    public string GameUrl { get; set; } = string.Empty;

    [JsonPropertyName("minimum_system_requirements")]
    public MinimumSystemRequirements? MinimumSystemRequirements { get; set; }
}

public class MinimumSystemRequirements
{
    [JsonPropertyName("memory")]
    public string Memory { get; set; } = string.Empty;
}