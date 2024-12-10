using ChessTournament.API.DTO.Member;
using ChessTournament.API.DTO.Tournament;
using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Models;

namespace ChessTournament.API.Mappers;

public static class TournamentMapper
{
    public static TournamentViewDTO ToDTO(this Tournament tournament)
    {
        return new TournamentViewDTO()
        {
            Id = tournament.Id,
            Name = tournament.Name,
            Place = tournament.Place,
            PlayerMin = tournament.PlayerMin,
            PlayerMax = tournament.PlayerMax,
            RegisteredPLayers = tournament.Members.Count,
            EloMin = tournament.EloMin,
            EloMax = tournament.EloMax,
            State = tournament.State,
            ActualRound = tournament.ActualRound,
            WomenOnly = tournament.WomenOnly,
            RegistrationEndDate = tournament.RegistrationEndDate,
            CreationDate = tournament.CreationDate,
            UpdateDate = tournament.UpdateDate,
            Categories = tournament.Categories?.Select(c => new CategoryValueDTO { Name = c.Name.ToString() }).ToList() ?? new List<CategoryValueDTO>(),
            Members = tournament.Members?.Select(m => MemberMapper.ToListDTO(m)).ToList() ?? new List<MemberViewDTO>(),
            Games = tournament.Games.ToListDTO()
        };
    }
    
    public static Tournament ToTournament(this TournamentFormDTO tournamentFormDTO)
    {
        return new Tournament
        {
            Name = tournamentFormDTO.Name,
            Place = tournamentFormDTO.Place,
            PlayerMin = tournamentFormDTO.PlayerMin,
            PlayerMax = tournamentFormDTO.PlayerMax,
            EloMin = tournamentFormDTO.EloMin,
            EloMax = tournamentFormDTO.EloMax,
            State = TournamentState.WaitingForPlayer,
            ActualRound = 0,
            WomenOnly = tournamentFormDTO.WomenOnly,
            RegistrationEndDate = tournamentFormDTO.RegistrationEndDate,
            CreationDate = DateTime.Today,
            UpdateDate = DateTime.Now,
            Games = new List<Game>(),
            Members = new List<Member>()
        };
    }

    public static TournamentPlayerView ToTournamentPlayerView(this Tournament tournament)
    {
        return new TournamentPlayerView()
        {
            Id = tournament.Id,
            Name = tournament.Name,
            Place = tournament.Place,
            State = tournament.State,
            ActualRound = tournament.ActualRound,
            RegistrationEndDate = tournament.RegistrationEndDate
        };
    }

    public static List<CategoryEnum> ToCategoryEnums(this List<CategoryViewDTO> categoryViewDTOs)
    {
        return categoryViewDTOs.Select(c => c.Name).ToList();
    }
}
