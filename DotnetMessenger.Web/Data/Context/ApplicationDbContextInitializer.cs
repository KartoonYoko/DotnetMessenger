using Microsoft.EntityFrameworkCore;

namespace DotnetMessenger.Web.Data.Context;

public static class InitializerExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app
            .Services
            .CreateScope();

        var initializer = scope
            .ServiceProvider
            .GetRequiredService<ApplicationDbContextInitializer>();

        await initializer.InitialiseAsync();
    }
}

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}