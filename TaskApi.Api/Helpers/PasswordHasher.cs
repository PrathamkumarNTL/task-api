using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public class PasswordHasher
{
    public static string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128/8);

        return Convert.ToBase64String(KeyDerivation.Pbkdf2(password,salt,KeyDerivationPrf.HMACSHA256,iterationCount:100000,numBytesRequested:256/8));
    }
}