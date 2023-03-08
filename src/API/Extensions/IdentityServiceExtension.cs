using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class IdentityServiceExtension
{
    public static async Task AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        var identityConnection = config.GetConnectionString("IdentityConnection");

        services.AddDbContext<FragIdentityDbContext>(opt =>
        {
            opt.UseSqlServer(identityConnection);
        });

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Password configuration
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 4;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 1;
        })
            .AddEntityFrameworkStores<FragIdentityDbContext>()
            .AddDefaultTokenProviders();

        var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();

        try
        {

        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occured during migration");
        }
        

    }
}