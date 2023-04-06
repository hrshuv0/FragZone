using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class AppDbInitializer
{
    public static async Task SeedAsync(FragDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Categories!.Any())
            {
                var categoryData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/category.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

                foreach (var item in categories!)
                {
                    context.Categories!.Add(item);
                }
                await context.SaveChangesAsync();
            }

            if (!context.Publishers!.Any())
            {
                var publisherData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/publisher.json");
                var publishers = JsonSerializer.Deserialize<List<Publisher>>(publisherData);
                
                foreach (var item in publishers!)
                {
                    context.Publishers!.Add(item);
                }
                await context.SaveChangesAsync();
            }

            if (!context.Games!.Any())
            {
                var gameData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/game.json");
                var games = JsonSerializer.Deserialize<List<Game>>(gameData);
                
                foreach (var item in games!)
                {
                    context.Games!.Add(item);
                }
                await context.SaveChangesAsync();
            }

        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<AppDbInitializer>();
            logger.LogError(e.Message);
        }
    }
}