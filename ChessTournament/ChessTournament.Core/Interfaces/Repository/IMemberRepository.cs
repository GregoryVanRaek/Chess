﻿using ChessTournament.Domain.Models;

namespace ChessTournament.Applications.Interfaces.Repository;

public interface IMemberRepository : IRepository<int, Member>
{
    public Task<Member?> GetOneByEmailOrUsernameAsync(string mail, string username);
    public Task<Member?> GetOneByEmailOrUsernameAsync(string username);
}