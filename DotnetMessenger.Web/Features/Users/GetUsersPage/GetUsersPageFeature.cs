using DotnetMessenger.Web.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DotnetMessenger.Web.Features.Users.GetUsersPage;

public record GetUsersPageRequest(
    int Skip,
    int Take);

public record GetUsersPageResponseItem(
    long UserId,
    string UserLogin);

public class GetUsersPageFeature(ApplicationDbContext context)
{
    public async Task<List<GetUsersPageResponseItem>> GetUsersPageAsync(
        GetUsersPageRequest request,
        CancellationToken cancellationToken)
    {
        var page = await context
            .Users
            .OrderBy(x => x.Login)
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(cancellationToken);
        
        List<GetUsersPageResponseItem> result = new(page.Count);

        foreach (var user in page)
        {
            result.Add(new GetUsersPageResponseItem(user.Id, user.Login));
        }

        return result;
    }
}