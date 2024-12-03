using System.Security.Cryptography;
using ChessTournament.Applications.Interfaces.Service;
using Isopoh.Cryptography.Argon2;

namespace ChessTournament.Applications.Services;

public class PasswordService : IPasswordService
{
    public string GenerateSalt()
    {
        byte[] saltBytes = new byte[128];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    public string HashPassword(string password)
    {
        string saltedPassword = GenerateSalt() + password;
        return Argon2.Hash(saltedPassword); 
    }

    public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
    {
        string saltedPassword = storedSalt + enteredPassword;
        string hashedPassword = Argon2.Hash(saltedPassword);
        return hashedPassword == storedHash;
    }
}
