using ChessTournament.Domain.Models;

namespace ChessTournament.Applications.Interfaces.Repository;

public interface ITournamentRepository :IRepository<int, Tournament>
{
}