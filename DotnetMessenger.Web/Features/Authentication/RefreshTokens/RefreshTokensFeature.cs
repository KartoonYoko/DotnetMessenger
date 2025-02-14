using DotnetMessenger.Web.Common.Services.JwtServices;
using DotnetMessenger.Web.Data.Context;
using DotnetMessenger.Web.Data.Entities;
using DotnetMessenger.Web.Features.Authentication.RefreshTokens.Errors;
using Microsoft.EntityFrameworkCore;

namespace DotnetMessenger.Web.Features.Authentication.RefreshTokens;

public record RefreshTokensRequest(string RefreshToken);

public sealed record RefreshTokensResponse(string AccessToken, string RefreshToken);

public class RefreshTokensFeature(
    ApplicationDbContext context,
    JwtHandlerService jwtHandlerService)
{
    public async Task<RefreshTokensResponse> Refresh(
        RefreshTokensRequest request,
        CancellationToken cancellationToken)
    {
        var userRefreshToken = await context
            .UserRefreshTokens
            .AsNoTracking()
            .Include(x => x.User)
            .Where(x => x.Token == request.RefreshToken)
            .FirstOrDefaultAsync(cancellationToken);

        if (userRefreshToken?.User is null)
            throw new RefreshTokenOrUserNotFoundException();

        var user = userRefreshToken.User;
        
        await context
            .UserRefreshTokens
            .Where(x => x.Token == request.RefreshToken)
            .ExecuteDeleteAsync(cancellationToken);

        var refreshToken = await CreateRefreshTokenAsync(user, cancellationToken);
        
        var accessToken = jwtHandlerService.CreateToken(userRefreshToken.User);
        
        return new RefreshTokensResponse(accessToken, refreshToken.Token);
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