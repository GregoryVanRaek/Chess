using System.Security.Cryptography;
using ChessTournament.Applications.Interfaces.Service;
using Isopoh.Cryptography.Argon2;

namespace ChessTournament.Applications.Services;

public class PasswordService : IPasswordService
{
    public string GenerateSalt(string email)
    {
        if (string.IsNullOrEmpty(email) || email.Length < 6)
            throw new ArgumentException("Email must have at least 6 characters.", nameof(email));
        
        return email.Substring(0, 6); 
    }

    public string HashPassword(string password, string mail)
    {
        string saltedPassword = GenerateSalt(mail) + password;
        return Argon2.Hash(saltedPassword); 
    }

    public bool VerifyPassword(string enteredPassword, string storedHash, string email)
    {
        string salt = GenerateSalt(email);
        string saltedPassword = salt + enteredPassword;
        string hashedPassword = Argon2.Hash(saltedPassword);
        return hashedPassword == storedHash;
    }
}
