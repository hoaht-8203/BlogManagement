using System;
using System.Security.Cryptography;
using System.Text;

namespace blog_server.Helpers;

public static class PasswordHelper
{
    public static string HashPassword(string password)
    {
        // Generate a random salt
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Hash the password with the salt
        using var hmac = new HMACSHA512(salt);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        // Combine salt and hash
        var hashBytes = new byte[salt.Length + hash.Length];
        Array.Copy(salt, 0, hashBytes, 0, salt.Length);
        Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);

        return Convert.ToBase64String(hashBytes);
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        try
        {
            // Convert the stored hash back to bytes
            var hashBytes = Convert.FromBase64String(storedHash);

            // Extract the salt (first 16 bytes)
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Hash the input password with the same salt
            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Compare the computed hash with the stored hash
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != hashBytes[i + 16])
                    return false;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
