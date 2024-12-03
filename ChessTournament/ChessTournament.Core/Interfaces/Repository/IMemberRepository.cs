using ChessTournament.Domain.Models;

namespace ChessTournament.Applications.Interfaces.Repository;

public interface IMemberRepository : IRepository<int, Member>
{
}