using System.Security.Claims;

namespace DotnetMessenger.Web.Common.Services.UserManager;

public class UserManagerService(IHttpContextAccessor httpContextAccessor)
{
    public long GetRequiredUserId()
    {
        var claim = httpContextAccessor
            .HttpContext?
            .User
            .Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

        if (!long.TryParse(claim?.Value, out var userId))
            throw new Exception("Claim is not found");
        
        return userId;
    }
}