namespace ChessTournament.API.DTO.Tournament;

public class PlayerScoreView
{
    public string Username { get; set;}
    public int GamesPlayed { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Ties { get; set; }
    public double Score { get; set; }
}