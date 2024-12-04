using ChessTournament.API.DTO.Tournament;
using ChessTournament.Domain.Models;

namespace ChessTournament.API.Mappers;

public static class TournamentMapper
{
    public static TournamentViewDTO ToListDTO(this Tournament tournament)
    {
        return new TournamentViewDTO()
        {
            Id = tournament.Id,
            Name = tournament.Name,
            Place = tournament.Place,
            PlayerMin = tournament.PlayerMin,
            PlayerMax = tournament.PlayerMax,
            EloMin = tournament.EloMin,
            EloMax = tournament.EloMax,
            State = tournament.State,
            ActualRound = tournament.ActualRound,
            WomenOnly = tournament.WomenOnly,
            RegistrationEndDate = tournament.RegistrationEndDate,
            CreationDate = tournament.CreationDate,
            UpdateDate = tournament.UpdateDate,
        };
    }
}