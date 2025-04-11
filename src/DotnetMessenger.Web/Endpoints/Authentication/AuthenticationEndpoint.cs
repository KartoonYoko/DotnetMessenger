using DotnetMessenger.Web.Features.Authentication.Login;
using DotnetMessenger.Web.Features.Authentication.Login.Errors;
using DotnetMessenger.Web.Features.Authentication.Logout;
using DotnetMessenger.Web.Features.Authentication.RefreshTokens;
using DotnetMessenger.Web.Features.Authentication.RefreshTokens.Errors;
using DotnetMessenger.Web.Features.Authentication.Register;
using DotnetMessenger.Web.Features.Authentication.Register.Errors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;

namespace DotnetMessenger.Web.Endpoints.Authentication;

public static class AuthenticationEndpoint
{
    public static RouteGroupBuilder MapAuthenticationEndpoints(this RouteGroupBuilder mainGroup)
    {
        var group = mainGroup.MapGroup("/authentication");

        group
            .MapPost("/register", Register)
            .WithSummary("Register");
        group
            .MapPost("/login", Login)
            .WithSummary("Login")
            .Produces<UnauthorizedLoginError>(401);
        group
            .MapPost("/logout", Logout)
            .WithSummary("Logout")
            .RequireAuthorization();
        group
            .MapPost("/refresh-token", RefreshToken)
            .WithSummary("Refresh token");

        return group;
    }
    
    private static async Task<Results<Ok<RefreshTokensResponse>, NotFound>> RefreshToken(
        [FromServices] RefreshTokensFeature service,
        [FromBody] RefreshTokensRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await service.Refresh(
                request, 
                cancellationToken);
            
            return TypedResults.Ok(result);
        }
        catch (RefreshTokenOrUserNotFoundException)
        {
            return TypedResults.NotFound();
        }
    }
    
    private static async Task<Ok> Logout(
        [FromServices] LogoutFeature service,
        [FromBody] LogoutRequest request,
        CancellationToken cancellationToken)
    {
        await service.LogoutAsync(
            request, 
            cancellationToken);
            
        return TypedResults.Ok();
    }

    private static async Task<Results<Ok<RegisterResponse>, Conflict>> Register(
        [FromServices] RegisterFeature service,
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await service.RegisterAsync(
                request, 
                cancellationToken);
            
            return TypedResults.Ok(response);
        }
        catch (RegisterExceptionBase ex)
        {
            if (ex is LoginAlreadyExistsException)
                return TypedResults.Conflict();

            throw;
        }
    }

    private static async Task<Results<Ok<LoginResponse>, ProblemHttpResult>> Login(
        [FromServices] LoginFeature service,
        [FromServices] Tracer tracer,
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        using var s = tracer.StartActiveSpan("AuthenticationEndpoint.Login");
        
        try
        {
            var result = await service.LoginAsync(
                request, 
                cancellationToken);
            
            return TypedResults.Ok(result);
        }
        catch (LoginExceptionBase ex)
        {
            s.SetStatus(Status.Error);

            if (ex is UnauthorizedException)
            {
                s.AddEvent("not successful login");
                s.SetAttribute("user.login", request.Login);

                return TypedResults.Problem(new UnauthorizedLoginError(
                    new UnauthorizedLoginError.ExtensionExampleObject("some value")));
            }
            
            s.RecordException(ex);

            throw;
        }
    }
}

internal class UnauthorizedLoginError : ProblemDetails
{
    internal UnauthorizedLoginError(ExtensionExampleObject extensionExample)
    {
        ExtensionExample = extensionExample;
        Title = "NotAuthorized";
        Status = 401;
    }

    public class ExtensionExampleObject(string value)
    {
        public string Value { get; set; } = value;
    }

    public ExtensionExampleObject ExtensionExample { get; set; }
}