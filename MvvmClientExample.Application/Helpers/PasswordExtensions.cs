using System.Security.Cryptography;
using System.Text;

namespace MvvmClientExample.Application.Helpers
{
    public static class PasswordExtensions
    {
        public static string ToHash(this string password)
        {
            var hasher = SHA256.Create();

            var passwordBytes = Encoding.UTF8.GetBytes(password);

            var hash = hasher.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hash);
        }
    }
}
