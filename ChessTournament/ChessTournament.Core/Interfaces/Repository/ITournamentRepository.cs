using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Models;

namespace ChessTournament.Applications.Interfaces.Repository;

public interface ITournamentRepository :IRepository<int, Tournament>
{
    Task<List<Category>> MapCategoriesAsync(List<CategoryEnum> categoryEnums);
    IEnumerable<Tournament> GetSomeTournament(int number);
}