using DotnetMessenger.Web.Features.Users.GetUsersPage;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMessenger.Web.Endpoints.Users;

public static class UsersEndpoint
{
    public static RouteGroupBuilder MapUsersEndpoints(this RouteGroupBuilder mainGroup)
    {
        var group = mainGroup.MapGroup("/users");

        group.MapGet("", GetUsers)
            .WithSummary("Get application users page")
            .WithDescription("Endpoint allow to get application users by page");
        
        return group;
    }
    
    private static async Task<Ok<List<GetUsersPageResponseItem>>> GetUsers(
        [FromServices] GetUsersPageFeature service,
        [FromQuery] int skip,
        [FromQuery] int take,
        CancellationToken cancellationToken)
    {
        var result = await service.GetUsersPageAsync(
            new GetUsersPageRequest(skip, take), 
            cancellationToken);
            
        return TypedResults.Ok(result);
    }
}