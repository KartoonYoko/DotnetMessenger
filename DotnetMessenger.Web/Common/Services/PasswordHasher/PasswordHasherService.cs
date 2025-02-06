using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;

namespace DotnetMessenger.Web.Common.Services.PasswordHasher;

public class PasswordHasherService(
    IOptions<PasswordHasherOptions> options)
{
    public string HashPassword(string password)
    {
        var encoding = Encoding.UTF8;
        
        var byteArray = encoding.GetBytes(password + options.Value.Salt);
        
        using var memoryStream = new MemoryStream(byteArray);
        
        using var md5 = MD5.Create();
        
        var hashBytes = md5.ComputeHash(memoryStream);

        return encoding.GetString(hashBytes);
    }
    
    public bool VerifyPassword(string password, string hashedPassword)
    {
        return hashedPassword == HashPassword(password);
    }
}