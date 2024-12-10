using System.ComponentModel.DataAnnotations;

namespace ChessTournament.API.DTO.Tournament;

public class AddPlayerDTO
{
    [Required]
    public int tournamentId { get; set; }
    [Required]
    public int playerId { get; set; }
}