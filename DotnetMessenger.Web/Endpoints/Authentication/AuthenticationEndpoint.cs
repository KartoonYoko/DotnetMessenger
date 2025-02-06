using DotnetMessenger.Web.Features.Authentication.Login;
using DotnetMessenger.Web.Features.Authentication.Login.Errors;
using DotnetMessenger.Web.Features.Authentication.Register;
using DotnetMessenger.Web.Features.Authentication.Register.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMessenger.Web.Endpoints.Authentication;

public static class AuthenticationEndpoint
{
    public static void MapAuthenticationEndpoints(this RouteGroupBuilder mainGroup)
    {
        var group = mainGroup.MapGroup("/authentication");

        group.MapPost("/register", Register);
        group.MapPost("/login", Login);
    }

    private static async Task<IResult> Register(
        [FromServices] RegisterFeature service,
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await service.RegisterAsync(request, cancellationToken);
            
            return Results.Ok(response);
        }
        catch (RegisterExceptionBase ex)
        {
            if (ex is LoginAlreadyExistsException)
                return Results.Conflict();

            throw;
        }
    }

    private static async Task<IResult> Login(
        [FromServices] LoginFeature service,
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await service.LoginAsync(request, cancellationToken);
            
            return Results.Ok(result);
        }
        catch (LoginExceptionBase ex)
        {
            if (ex is UnauthorizedException)
                return Results.Unauthorized();

            throw;
        }
    }
}