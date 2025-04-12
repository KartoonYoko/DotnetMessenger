using System.Net;
using System.Net.Http.Json;
using DotnetMessenger.Web.Data.Context;
using DotnetMessenger.Web.Features.Authentication.Register;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetMessenger.Web.Test.Endpoints;

public class AuthenticationEndpointTests(
    CustomWebApplicationFactory factory,
    ITestOutputHelper testOutputHelper)
    : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask InitializeAsync()
    {
        using var scope = factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE users, user_refresh_tokens RESTART IDENTITY CASCADE");
        
        return ValueTask.CompletedTask;
    }
    
    [Fact]
    public async Task Should_RegisterReturnsCreated()
    {
        var url = $"{factory.Server.BaseAddress}api/authentication/register";
        
        var client = factory.CreateClient();

        RegisterRequest request = new("login", "password");
        var response = await client.PostAsync(
            url,
            JsonContent.Create(request), 
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}