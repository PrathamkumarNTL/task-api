using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public static class PasswordHasher
{
    public static string Hash(string password,out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(128/8);

        return Convert.ToBase64String(KeyDerivation.Pbkdf2(password,salt,KeyDerivationPrf.HMACSHA256,iterationCount:100_1000,numBytesRequested:256/8));
    }

    public static bool Verify(string password,string storedHash,byte[] salt)
    {
        var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password,salt,KeyDerivationPrf.HMACSHA256,iterationCount:100_1000,numBytesRequested:256/8));

        return hash == storedHash;
    }
}