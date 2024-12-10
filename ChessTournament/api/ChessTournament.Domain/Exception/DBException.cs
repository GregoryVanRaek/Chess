namespace ChessTournament.Domain.Exception;

public class DBException : System.Exception
{
    public DBException(string message) : base(message){}
}