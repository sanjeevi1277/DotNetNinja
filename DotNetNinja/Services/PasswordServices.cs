using Microsoft.AspNetCore.Identity;

namespace DotNetNinja.Services
{

    public static class PasswordServices
    {
        private static readonly PasswordHasher<object> hasher = new();

        public static string HashPassword(string password)
        {
            return hasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            var result = hasher.VerifyHashedPassword(null, hashedPassword, plainPassword);
            return result == PasswordVerificationResult.Success;
        }
    }

}
