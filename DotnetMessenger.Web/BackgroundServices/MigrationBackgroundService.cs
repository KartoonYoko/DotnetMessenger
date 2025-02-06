using DotnetMessenger.Web.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotnetMessenger.Web.BackgroundServices;

public class MigrationBackgroundService(IServiceProvider services) 
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await MigrateAsync(cancellationToken);
    }
    
    private async Task MigrateAsync(CancellationToken cancellationToken)
    {
        using var scope = services.CreateScope();
        
        var context = scope
            .ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync(cancellationToken);
    }
}