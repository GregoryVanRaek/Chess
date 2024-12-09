using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Models;

namespace ChessTournament.Applications.Interfaces.Repository;

public interface ITournamentRepository :IRepository<int, Tournament>
{
    Task<List<Category>> MapCategoriesAsync(List<CategoryEnum> categoryEnums);
    IEnumerable<Tournament> GetSomeTournament(int number);
    Task<int> AddPlayers(Tournament tournament, Member member);
    Task<int> RemovePlayers(Tournament tournament, Member member);
    Task<Tournament> StartTournament(Tournament entity);
}