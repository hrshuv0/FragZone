using API.Helpers;
using Core.Interfaces;
using Core.Repositories;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static async Task AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {

        #region Database CONFIG
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<FragDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        #endregion

        services.AddAutoMapper(typeof(MappingProfiles));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAuthService, AuthService>();


        var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();

        try
        {
            // Migrate and Seed Data
            
            var context = services.BuildServiceProvider().GetRequiredService<FragDbContext>();
            await context.Database.MigrateAsync();
            
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(e, "An error occured during migration");
        }
    }
}