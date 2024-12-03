namespace ChessTournament.Applications.Interfaces.Service;

public interface IPasswordService
{
    string GenerateSalt();
    string HashPassword(string password);
    bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt);
}