using System.Text.Json;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public static class SeedUserData
{
    public static async Task SeedUserAsync(FragIdentityDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Users.Any())
            {
                var userData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/user.json");
                var users = JsonSerializer.Deserialize<List<ApplicationUser>>(userData);
                
                if (users == null) return;
                
                foreach (var user in users)
                {
                    user.UserName = user.UserName!.ToLower();
                    user.Email = user.Email!.ToLower();
                    await context.Users.AddAsync(user);
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