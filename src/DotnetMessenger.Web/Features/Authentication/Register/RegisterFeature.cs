using DotnetMessenger.Web.Common.Services.JwtServices;
using DotnetMessenger.Web.Common.Services.PasswordHasher;
using DotnetMessenger.Web.Data.Context;
using DotnetMessenger.Web.Data.Entities;
using DotnetMessenger.Web.Features.Authentication.Register.Errors;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DotnetMessenger.Web.Features.Authentication.Register;

public sealed record RegisterRequest(string Login, string Password);

public sealed record RegisterResponse(string AccessToken, string RefreshToken);

public sealed class RegisterFeature(
    ApplicationDbContext context,
    JwtHandlerService jwtHandlerService,
    PasswordHasherService passwordHasherService)
{
    public async Task<RegisterResponse> RegisterAsync(
        RegisterRequest request, 
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            Login = request.Login,
            Password = passwordHasherService.HashPassword(request.Password),
        };
        
        await context.Users.AddAsync(user, cancellationToken);

        try
        {
            var userRefreshToken = await CreateRefreshTokenAsync(
                user, 
                cancellationToken);
            
            await context.SaveChangesAsync(cancellationToken);
            
            var accessToken = jwtHandlerService.CreateToken(user);

            return new RegisterResponse(accessToken, userRefreshToken.Token);
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is not PostgresException pgEx)
                throw;

            if (pgEx.SqlState == "23505")
                throw new LoginAlreadyExistsException();
            
            throw;
        }
    }
    
    private async Task<UserRefreshToken> CreateRefreshTokenAsync(
        User user,
        CancellationToken cancellationToken)
    {
        var userRefreshToken = new UserRefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(7),
            User = user,
        };
        
        await context
            .UserRefreshTokens
            .AddAsync(
                userRefreshToken, 
                cancellationToken);

        return userRefreshToken;
    }
}