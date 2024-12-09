namespace ChessTournament.API.DTO.Game;

public class GameViewDTO
{
    public int RoundNumber { get; set; }
    public List<PlayerViewDTO> Players { get; set; } = new List<PlayerViewDTO>();
}

public class PlayerViewDTO
{
    public string Username { get; set; }
    public string Color { get; set; }
}