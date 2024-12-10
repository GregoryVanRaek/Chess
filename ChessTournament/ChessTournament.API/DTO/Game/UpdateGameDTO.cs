using System.ComponentModel.DataAnnotations;
using ChessTournament.Domain.Enum;

namespace ChessTournament.API.DTO.Game;

public class UpdateGameDTO
{
    [Required] 
    public int TournamentId { get; set; }
    [Required]
    public int GameId { get; set; }
    [Required]
    public GameEnum Result { get; set; }
}