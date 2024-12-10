namespace ChessTournament.Domain.Exception;

public class AlreadyExistException : System.Exception
{
    public AlreadyExistException(string message):base(message) { }
}