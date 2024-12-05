using ChessTournament.Domain.Enum;

namespace ChessTournament.Domain.Models;

public class Tournament
{
    public int? Id { get; set; }
    public required string Name { get; set; }
    public string? Place { get; set; }
    public required int PlayerMin { get; set; }
    public required int PlayerMax { get; set; } 
    public int? EloMin { get; set; }
    public int? EloMax { get; set; }
    public required TournamentState State { get; set; }
    public required int ActualRound { get; set; } 
    public required bool WomenOnly { get; set; } 
    public required DateTime RegistrationEndDate {get; set;} 
    public required DateTime CreationDate {get; set;} 
    public required DateTime UpdateDate {get; set;}
    public List<Category> Categories { get; set; } 
}