using DotnetMessenger.Web.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotnetMessenger.Web.Features.Authentication.Logout;

public record LogoutRequest(string RefreshToken); 

public class LogoutFeature(ApplicationDbContext context)
{
    public async Task LogoutAsync(
        LogoutRequest request,
        CancellationToken cancellationToken)
    {
        await context
            .UserRefreshTokens
            .Where(x => x.Token == request.RefreshToken)
            .ExecuteDeleteAsync(cancellationToken);
    }
}