using ChessTournament.Domain.Enum;

namespace ChessTournament.Domain.Models;

public class Member
{
    public int? Id { get; set; }
    public required string Username { get; set; }
    public required string Mail { get; set; }
    public required string Password { get; set; }
    public required DateTime Birthday { get; set; }
    public required Gender Gender { get; set; }
    public int? Elo { get; set; }
    public Role Role { get; set; } = Role.User;
    
    public List<Tournament> Tournaments { get; set; } = new List<Tournament>();
    
    public List<GameMember> GameMembers { get; set; } = new List<GameMember>();
}