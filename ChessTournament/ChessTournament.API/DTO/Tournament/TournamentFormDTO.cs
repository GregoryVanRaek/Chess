using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Models;

namespace ChessTournament.API.DTO.Tournament;

public class TournamentFormDTO
{
    public required string Name { get; set; }
    public string? Place { get; set; }
    public required int PlayerMin { get; set; }
    public required int PlayerMax { get; set; }
    public int? EloMin { get; set; }
    public int? EloMax { get; set; }
    public required bool WomenOnly { get; set; } 
    public required DateTime RegistrationEndDate {get; set;}
    public List<CategoryViewDTO> Categories { get; set; }
}