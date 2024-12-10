using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Models;

namespace ChessTournament.Applications.Interfaces.Service;

public interface ITournamentService : IService<int, Tournament>
{
    Task<Tournament> CreateAsync(Tournament entity, List<CategoryEnum> categoryEnums);
    Task<bool> DeleteAsync(Tournament entity);
    IEnumerable<Tournament> GetSomeTournament(int number);
    Task<int> AddPlayer(Tournament tournament, Member member);
    Task<int> RemovePlayer(Tournament tournament, Member member);
    Task<Tournament> StartTournament(Tournament toStart);
    Task<Tournament> UpdateResult(Game gameToUpdate);
    Task<Tournament> UpdateAsync(Tournament entity);
    Task<IEnumerable<Game>> GetScore(Tournament t);
}