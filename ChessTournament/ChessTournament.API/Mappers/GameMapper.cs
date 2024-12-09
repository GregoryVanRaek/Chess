using ChessTournament.API.DTO.Game;
using ChessTournament.Domain.Models;

namespace ChessTournament.API.Mappers;

public static class GameMapper
{
    public static GameViewDTO ToDTO(this Game game)
    {
        return new GameViewDTO
        {
            RoundNumber = game.RoundNumber,
            Players = game.GameMembers.Select(gm => gm.ToDTO()).ToList()
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
}