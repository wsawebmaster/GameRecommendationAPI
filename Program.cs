using Microsoft.EntityFrameworkCore;
  using GameRecommendationAPI.Data;
  using GameRecommendationAPI.GameRecommendations;
  using GameRecommendationAPI.Services;

  var builder = WebApplication.CreateBuilder(args);

  // Add services to the container.
  builder.Services.AddOpenApi();
  builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});
  builder.Services.AddHttpClient<GameApiService>();
  builder.Services.AddDbContext<AppDbContext>();

  var app = builder.Build();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
      app.MapOpenApi();
      app.UseSwagger();
      app.UseSwaggerUI();
  }

  // Comentar temporariamente para evitar problemas com HTTPS
  // app.UseHttpsRedirection();

  // Configurando as rotas da API
//   app.MapGet("/", () => "Hello-World! Bem-vindo à API de recomendação de jogos!");
app.AddGameRecommendationRoutes();

  app.Run();
