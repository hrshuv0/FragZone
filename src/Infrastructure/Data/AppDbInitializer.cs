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
                var categoryData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

                foreach (var item in categories!)
                {
                    context.Categories!.Add(item);
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