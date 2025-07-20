namespace GameRecommendationAPI.Models;

public class FreeToPlayGame
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string GameUrl { get; set; } = string.Empty;
    public MinimumSystemRequirements? MinimumSystemRequirements { get; set; }
}

public class MinimumSystemRequirements
{
    public string Memory { get; set; } = string.Empty;
}