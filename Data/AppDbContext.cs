using Microsoft.EntityFrameworkCore;
using GameRecommendationAPI.Models;

namespace GameRecommendationAPI.Data;

public class AppDbContext : DbContext
{
    public DbSet<Game> Games { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=GameRecommendation.db");
        }
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}