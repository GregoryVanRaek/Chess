using ChessTournament.Domain.Enum;

namespace ChessTournament.API.DTO.Game;

public class GameViewDTO
{
    public int? Id { get; set; }
    public int RoundNumber { get; set; }
    public List<PlayerViewDTO> Players { get; set; } = new List<PlayerViewDTO>();
    public GameEnum Result { get; set; }
}

public class PlayerViewDTO
{
    public string Username { get; set; }
    public string Color { get; set; }
}