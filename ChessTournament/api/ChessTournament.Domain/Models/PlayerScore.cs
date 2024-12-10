namespace ChessTournament.Domain.Models;

public class PlayerScore
{
    public string Username { get; set; }
    public int GamesPlayed { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Ties { get; set; }
    public double Score { get; set; }
}