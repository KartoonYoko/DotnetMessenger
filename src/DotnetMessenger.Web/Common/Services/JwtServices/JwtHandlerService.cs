using System.Security.Claims;
using System.Text;
using DotnetMessenger.Web.Data.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace DotnetMessenger.Web.Common.Services.JwtServices;

public sealed class JwtHandlerService(
    IOptions<JwtHandlerOptions> options)
{
    public string CreateToken(User user)
    {
        var secretKey = options.Value.SecretKey;
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Login),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(options.Value.ExpirationInMinutes),
            SigningCredentials = credentials,
            Issuer = options.Value.Issuer,
            Audience = options.Value.Audience,
        };
        
        return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
    }
}