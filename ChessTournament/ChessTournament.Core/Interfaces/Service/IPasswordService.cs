namespace ChessTournament.Applications.Interfaces.Service;

public interface IPasswordService
{
    string GenerateSalt(string mail);
    string HashPassword(string password, string mail);
    bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt);
}