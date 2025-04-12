using DotnetMessenger.Web.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace DotnetMessenger.Web.Test;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program> , IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:17-alpine")
        .WithDatabase("dotnet_messenger_db")
        .WithUsername("dotnet_messenger")
        .WithPassword("dotnet_messenger")
        .Build();

    public async ValueTask InitializeAsync()
    {
        await _postgres.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _postgres.StopAsync();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Remove(services.Single(x => 
                typeof(DbContextOptions<ApplicationDbContext>) == x.ServiceType));
            
            services.AddDbContext<ApplicationDbContext>((container, options) =>
            {
                options.UseNpgsql(_postgres.GetConnectionString());
            });
        });

        builder.UseEnvironment("Development");
    }
}