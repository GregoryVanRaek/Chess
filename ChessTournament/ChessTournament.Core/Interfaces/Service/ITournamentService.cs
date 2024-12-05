﻿using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Models;

namespace ChessTournament.Applications.Interfaces.Service;

public interface ITournamentService : IService<int, Tournament>
{
    Task<Tournament> CreateAsync(Tournament entity, List<CategoryEnum> categoryEnums);
}