using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static async Task AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<FragDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        #region Dependency Injection

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #endregion
        

        var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();

        try
        {
            // Migrate and Seed Data
            
            var context = services.BuildServiceProvider().GetRequiredService<FragDbContext>();
            // await context.Database.MigrateAsync();
            
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(e, "An error occured during migration");
        }
    }
}