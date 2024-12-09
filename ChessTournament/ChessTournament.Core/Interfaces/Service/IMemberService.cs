using ChessTournament.Domain.Models;

namespace ChessTournament.Applications.Interfaces.Service;

public interface IMemberService : IService<int, Member>
{
    IAsyncEnumerable<Member> CheckParticipation(Tournament tournament);
    public bool CheckOneParticipation(Tournament tournament, Member member);
    public Task<Member> Login(string username, string password);
}