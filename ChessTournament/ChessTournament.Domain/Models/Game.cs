using ChessTournament.Domain.Enum;

namespace ChessTournament.Domain.Models;

public class Game
{
    public int? Id { get; set; }
    public int RoundNumber { get; set; }
    public GameEnum Result { get; set; }
    
    public int? TournamentId { get; set; }
    public Tournament? Tournament { get; set; }
    
    public List<GameMember> GameMembers { get; set; } = new List<GameMember>(); 
}