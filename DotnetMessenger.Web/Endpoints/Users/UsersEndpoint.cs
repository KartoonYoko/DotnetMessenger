using DotnetMessenger.Web.Features.Users.GetUsersPage;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMessenger.Web.Endpoints.Users;

public static class UsersEndpoint
{
    public static void MapUsersEndpoints(this RouteGroupBuilder mainGroup)
    {
        var group = mainGroup.MapGroup("/users");

        group.MapGet("", GetUserChats);
    }
    
    private static async Task<IResult> GetUserChats(
        [FromServices] GetUsersPageFeature service,
        [FromQuery] int skip,
        [FromQuery] int take,
        CancellationToken cancellationToken)
    {
        var result = await service.GetUsersPageAsync(
            new GetUsersPageRequest(skip, take), 
            cancellationToken);
            
        return Results.Ok(result);
    }
}