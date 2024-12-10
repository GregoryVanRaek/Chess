using ChessTournament.API.DTO.Game;
using ChessTournament.API.DTO.Tournament;
using ChessTournament.Domain.Models;

namespace ChessTournament.API.Mappers;

public static class GameMapper
{
    public static GameViewDTO ToDTO(this Game game)
    {
        return new GameViewDTO
        {
            Id = game.Id,
            RoundNumber = game.RoundNumber,
            Players = game.GameMembers.Select(gm => gm.ToDTO()).ToList(),
            Result = game.Result
        };
    }

    public static PlayerViewDTO ToDTO(this GameMember gameMember)
    {
        return new PlayerViewDTO
        {
            Username = gameMember.Member.Username,
            Color = gameMember.Color.ToString()
        };
    }

    public static List<GameViewDTO> ToListDTO(this IEnumerable<Game> games)
    {
        return games.Select(g => g.ToDTO()).ToList();
    }

    public static PlayerScoreView ToPlayerScoreView(this PlayerScore playerscore)
    {
        return new PlayerScoreView()
        {
            Username = playerscore.Username,
            GamesPlayed = playerscore.GamesPlayed,
            Wins = playerscore.Wins,
            Losses = playerscore.Losses,
            Ties = playerscore.Ties,
            Score = playerscore.Score
        };
    }
    
}