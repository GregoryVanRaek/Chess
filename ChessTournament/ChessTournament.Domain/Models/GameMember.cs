using ChessTournament.Domain.Enum;

namespace ChessTournament.Domain.Models;

public class GameMember
{
    public int MemberId { get; set; }
    public Member Member { get; set; }

    public int GameId { get; set; }
    public Game Game { get; set; }

    public ColorEnum Color { get; set; } 
}