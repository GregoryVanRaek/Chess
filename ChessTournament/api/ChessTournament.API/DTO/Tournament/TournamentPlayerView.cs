using ChessTournament.Domain.Enum;

namespace ChessTournament.API.DTO.Tournament;

public class TournamentPlayerView
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Place { get; set; }
    public TournamentState State { get; set; }
    public int ActualRound { get; set; }
    public DateTime RegistrationEndDate{ get; set; }
}