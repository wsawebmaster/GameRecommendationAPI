using Microsoft.AspNetCore.Builder;
  using Microsoft.EntityFrameworkCore;
  using GameRecommendationAPI.Data;
  using GameRecommendationAPI.Models;
  using GameRecommendationAPI.Services;

  namespace GameRecommendationAPI.GameRecommendations;

  public static class GameRecommendationsExtensions
  {
      public static void AddGameRecommendationRoutes(this WebApplication app)
      {
          app.MapGet("/games/recommend", async (GameApiService apiService, AppDbContext db, string genre, string? platform, int? maxRam) =>
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
                  return Results.Ok(response);
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
              Summary = "Recommends a free-to-play game based on user preferences",
              Description = "Returns a randomly selected game matching the specified genre (required), platform (optional: pc, browser, all), and maximum RAM in MB (optional)."
          });

          app.MapGet("/games/recommended", async (AppDbContext db) =>
          {
              var games = await db.Games.ToListAsync();
              return games.Any() ? Results.Ok(games) : Results.NotFound("Nenhum jogo recomendado encontrado.");
          })
          .WithName("GetRecommendedGames")
          .WithOpenApi(op => new(op)
          {
              Summary = "Lists all recommended games",
              Description = "Returns a list of all games previously recommended and saved in the database."
          });
      }
  }