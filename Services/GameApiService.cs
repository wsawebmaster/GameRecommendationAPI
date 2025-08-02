using System.Net.Http.Json;
using GameRecommendationAPI.Models;

namespace GameRecommendationAPI.Services;

public class GameApiService
{
    private readonly HttpClient _httpClient;

    public GameApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://www.freetogame.com/api/");
    }

    public async Task<List<FreeToPlayGame>> GetGamesAsync(string? genre, string? platform, int? maxRam)
    {
        // Validar plataforma
        if (!string.IsNullOrEmpty(platform) && platform.ToLower() != "pc" && platform.ToLower() != "browser" && platform.ToLower() != "all")
            return new List<FreeToPlayGame>();

        var query = "games";
        var parameters = new List<string>();
        if (!string.IsNullOrEmpty(genre))
            parameters.Add($"category={genre.ToLower()}");
        if (!string.IsNullOrEmpty(platform))
            parameters.Add($"platform={platform.ToLower()}");
        if (parameters.Any())
            query += "?" + string.Join("&", parameters);

        List<FreeToPlayGame> games = new();

        try
        {
            var response = await _httpClient.GetAsync(query);
            response.EnsureSuccessStatusCode();
            games = await response.Content.ReadFromJsonAsync<List<FreeToPlayGame>>() ?? new List<FreeToPlayGame>();

        }
        catch (HttpRequestException)
        {
            return new List<FreeToPlayGame>();
        }
        catch (Exception)
        {
            return new List<FreeToPlayGame>();
        }

        

        // Filtrar por RAM (se fornecido)
        if (maxRam.HasValue && maxRam > 0)
        {
            games = games.Where(g => g.MinimumSystemRequirements == null ||
                (TryParseRam(g.MinimumSystemRequirements.Memory, out int ram) && ram <= maxRam.Value))
                .ToList();
        }

        return games;
    }

    private bool TryParseRam(string? memory, out int ramMb)
    {
        ramMb = 0;
        if (string.IsNullOrEmpty(memory))
            return false;

        memory = memory.ToLower().Replace(" ", "");
        if (memory.Contains("gb"))
        {
            if (int.TryParse(memory.Replace("gb", ""), out int gb))
            {
                ramMb = gb * 1024;
                return true;
            }
        }
        else if (memory.Contains("mb"))
        {
            if (int.TryParse(memory.Replace("mb", ""), out ramMb))
                return true;
        }
        return false;
    }
}