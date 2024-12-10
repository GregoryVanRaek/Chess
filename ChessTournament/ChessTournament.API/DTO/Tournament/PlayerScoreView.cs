namespace ChessTournament.API.DTO.Tournament;

public class PlayerScoreView
{
    public string Username { get; set;}
    public int GamesPlayed { get; set; }
    public int Victory { get; set; }
    public int Defeat { get; set; }
    public int Tie { get; set; }
    public float Score { get; set; }
}