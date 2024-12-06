namespace ChessTournament.Applications.Interfaces.Service;

public interface IPasswordService
{
    string GenerateSalt(string mail);
    string HashPassword(string password, string mail);
    public bool VerifyPassword(string email, string enteredPassword, string storedHash);
}