using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using GameRecommendationAPI.Data;
using GameRecommendationAPI.Models;
using GameRecommendationAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace GameRecommendationAPI.GameRecommendations;

public static class GameRecommendationsExtensions
{
    public static void AddGameRecommendationRoutes(this WebApplication app)
    {
        app.MapGet("/games/recommend", async (
          GameApiService apiService, AppDbContext db,
          [SwaggerParameter("Gênero do jogo (obrigatório) ex.: Shooter, MMORPG...", Required = true)] string genre,
          [SwaggerParameter("Plataforma (opcional: pc, browser, all)")] string? platform,
          [SwaggerParameter("Memória RAM máxima em MB (opcional) ex.: 4096")] int? maxRam

          ) =>
        {
            // Validação de entrada
            if (string.IsNullOrEmpty(genre))
                return Results.BadRequest("É necessário informar pelo menos um gênero.");
            if (maxRam.HasValue && maxRam <= 0)
                return Results.BadRequest("O valor de maxRam deve ser um número positivo.");

            try
            {
                // Buscar jogos da API externa
                var games = await apiService.GetGamesAsync(genre, platform, maxRam);
                if (!games.Any())
                    return Results.NotFound("Nenhum jogo encontrado com os critérios informados.");

                // Selecionar um jogo aleatoriamente
                var random = new Random();
                var selectedGame = games[random.Next(games.Count)];

                // Salvar no banco
                var gameToSave = new Game
                {
                    Title = selectedGame.Title,
                    Genre = selectedGame.Genre
                };
                db.Games.Add(gameToSave);
                await db.SaveChangesAsync();

                // Retornar a recomendação
                var response = new GameRecommendationResponse
                {
                    Title = selectedGame.Title,
                    Genre = selectedGame.Genre,
                    GameUrl = selectedGame.GameUrl
                };
                return Results.Ok(new
                {
                    Title = selectedGame.Title,
                    GameUrl = selectedGame.GameUrl
                });
            }
            catch (HttpRequestException)
            {
                return Results.Problem(
                    detail: "Falha ao buscar jogos na API externa.",
                    title: "Erro na API Externa",
                    statusCode: StatusCodes.Status503ServiceUnavailable
                );
            }
            catch (Exception)
            {
                return Results.Problem(
                    detail: "Ocorreu um erro inesperado.",
                    title: "Erro Interno do Servidor",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
        })
        .WithName("GetGameRecommendation")
        .WithOpenApi(op => new(op)
        {
            Summary = "Recomenda um jogo gratuito com base nas preferências do utilizador",
            Description = "Devolve um jogo selecionado aleatoriamente que corresponde ao género especificado (obrigatório), à plataforma (opcional: pc, browser, todos) e à RAM máxima em MB (opcional)."
        });

        app.MapGet("/games/recommended", async (AppDbContext db) =>
        {
            var games = await db.Games.ToListAsync();
            return games.Any() ? Results.Ok(games) : Results.NotFound("Nenhum jogo recomendado encontrado.");
        })
        .WithName("GetRecommendedGames")
        .WithOpenApi(op => new(op)
        {
            Summary = "Lista todos os jogos recomendados",
            Description = "Lista todos os jogos recomendados."
        });
    }
}