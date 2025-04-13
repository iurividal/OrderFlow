using System.Security.Cryptography;
using System.Text;

namespace OrderFlow.Api.Extensions;

public static class ObjectValues
{
    public static string HashPassword(this string plain)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(plain);
        byte[] hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}