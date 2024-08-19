using System.Security.Cryptography;

namespace ITPLibrary.Core.Utilities
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 10000;

        public static string HashPassword(string password, out string salt)
        {
            using var rng = new RNGCryptoServiceProvider();
            var saltBytes = new byte[SaltSize];
            rng.GetBytes(saltBytes);
            salt = Convert.ToBase64String(saltBytes);

            using var DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, Iterations);
            var hash = DeriveBytes.GetBytes(HashSize);
            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);

            using var DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, Iterations);
            var hash = DeriveBytes.GetBytes(HashSize);
            return Convert.ToBase64String(hash) == storedHash;
        }
    }
}
