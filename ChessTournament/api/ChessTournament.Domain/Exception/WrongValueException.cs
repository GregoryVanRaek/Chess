namespace ChessTournament.Domain.Exception;

public class WrongValueException :System.Exception
{
    public WrongValueException(string message):base(message){}
}