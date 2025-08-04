using System.Security.Cryptography;
using System.Text;

namespace Core.Security;

public static class PasswordHash
{
    public static string Hash(this string password)
    {
        var sha512 = new SHA512Managed();
        var bytes = Encoding.UTF8.GetBytes(password);
        var passwordHash = sha512.ComputeHash(bytes);
        var hash=Encoding.UTF8.GetString(passwordHash);
        return hash;
    }
}