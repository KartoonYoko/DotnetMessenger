using DotnetMessenger.Web.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotnetMessenger.Web.Startup;

public static partial class Startup
{
    private static void AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<ApplicationDbContextInitializer>();
    }
}