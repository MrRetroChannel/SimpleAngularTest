using Microsoft.Identity.Client;
using System.Security.Cryptography;

namespace WebApplication1.Utility
{
    public class PasswordHasher
    {
        private static int SaltSize = 16;
        private static int KeySize = 32;
        private static int Iterations = 15000;

        private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;

        public static string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, hashAlgorithm, KeySize);

            return String.Join('.', Convert.ToBase64String(hash), Convert.ToBase64String(salt));
        }

        public static bool CheckValidity(string hashedPassword, string rawPassword)
        {
            var splitted = hashedPassword.Split('.');

            if (splitted.Length != 2)
                throw new ArgumentException("Invalid hashed password string");

            var salt = Convert.FromBase64String(splitted[1]); 

            var hash = Rfc2898DeriveBytes.Pbkdf2(rawPassword, salt, Iterations, hashAlgorithm, KeySize);

            return Convert.ToBase64String(hash) == splitted[0];
        }
    }
}
