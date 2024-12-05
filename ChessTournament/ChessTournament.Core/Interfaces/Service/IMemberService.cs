using ChessTournament.Domain.Models;

namespace ChessTournament.Applications.Interfaces.Service;

public interface IMemberService : IService<int, Member>
{
    IAsyncEnumerable<Member> CheckParticipation(Tournament tournament);
}