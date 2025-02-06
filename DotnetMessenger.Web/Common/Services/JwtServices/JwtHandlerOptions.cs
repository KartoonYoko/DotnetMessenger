namespace DotnetMessenger.Web.Common.Services.JwtServices;

public class JwtHandlerOptions
{
    public required string SecretKey { get; set; }
    
    public int ExpirationInMinutes { get; set; }
    
    public required string Issuer { get; set; }
    
    public required string Audience { get; set; }
}