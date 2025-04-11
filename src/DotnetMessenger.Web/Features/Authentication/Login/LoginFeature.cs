using DotnetMessenger.Web.Common.Services.JwtServices;
using DotnetMessenger.Web.Common.Services.PasswordHasher;
using DotnetMessenger.Web.Data.Context;
using DotnetMessenger.Web.Data.Entities;
using DotnetMessenger.Web.Features.Authentication.Login.Errors;
using Microsoft.EntityFrameworkCore;

namespace DotnetMessenger.Web.Features.Authentication.Login;

public sealed record LoginRequest(string Login, string Password);

public sealed record LoginResponse(string AccessToken, string RefreshToken);

public sealed class LoginFeature(
    ApplicationDbContext context,
    JwtHandlerService jwtHandlerService,
    PasswordHasherService passwordHasherService)
{
    public async Task<LoginResponse> LoginAsync(
        LoginRequest request, 
        CancellationToken cancellationToken)
    {
        var hashPassword = passwordHasherService.HashPassword(request.Password);
        
        var user = await context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.Login == request.Login
                && x.Password == hashPassword,
                cancellationToken);

        if (user is null)
            throw new UnauthorizedException();

        var refreshTokenModel = await CreateRefreshTokenAsync(
            user,
            cancellationToken);

        var accessToken = jwtHandlerService.CreateToken(user);
        
        return new LoginResponse(accessToken, refreshTokenModel.Token);
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
            UserId = user.Id,
        };
        
        await context
            .UserRefreshTokens
            .AddAsync(
                userRefreshToken, 
                cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);

        return userRefreshToken;
    }
}