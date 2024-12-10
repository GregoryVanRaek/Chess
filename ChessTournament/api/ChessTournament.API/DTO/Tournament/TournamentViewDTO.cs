using ChessTournament.API.DTO.Game;
using ChessTournament.API.DTO.Member;
using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Models;

namespace ChessTournament.API.DTO.Tournament;

public class TournamentViewDTO
{
    public int? Id { get; set; }
    public required string Name { get; set; }
    public string? Place { get; set; }
    public int? RegisteredPLayers { get; set; }
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
    public List<CategoryValueDTO> Categories { get; set; }
    public List<MemberViewDTO> Members { get; set; }
    public List<GameViewDTO> Games { get; set; } = new List<GameViewDTO>();
}