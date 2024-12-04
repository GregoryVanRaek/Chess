using ChessTournament.Applications.Interfaces.Repository;
using ChessTournament.Applications.Interfaces.Service;
using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Exception;
using ChessTournament.Domain.Models;

namespace ChessTournament.Applications.Services;

public class TournamentService :ITournamentService
{
    private readonly ITournamentRepository _tournamentRepository;

    public TournamentService(ITournamentRepository tournamentRepository)
    {
        this._tournamentRepository = tournamentRepository;
    }

    public async Task<List<Tournament>> GetAllAsync()
    {
        try
        {
            return await _tournamentRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Member service error: " + e.Message);
        }
    }

    public async Task<Tournament> GetOneByIdAsync(int key)
    {
        try
        {
            return await this._tournamentRepository.GetOneByIdAsync(key);
        }
        catch (Exception e)
        {
            throw new Exception("There was an error getting the tournament", e);
        }
    }

    public async Task<Tournament> CreateAsync(Tournament entity)
    {
        try
        {
            if(entity.PlayerMax < entity.PlayerMin )
                throw new WrongValueException("Player max can't be less than player min");
            
            if(entity.PlayerMax < entity.PlayerMin)
                throw new WrongValueException("Elo max can't be less than Elo min");

            if (entity.CreationDate.AddDays(entity.PlayerMin) < entity.RegistrationEndDate)
                throw new WrongValueException("The end date must be greater than the creation date + the number minimum of player");
                
            return await this._tournamentRepository.CreateAsync(entity);
        }
        catch (Exception e)
        {
            throw new Exception("Tournament service error : " + e.Message);
        }
    }

    public Task<Tournament> UpdateAsync(Tournament entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Tournament entity)
    {
        Tournament toDelete = await GetOneByIdAsync((int)entity.Id);
        
        if (toDelete is not null && toDelete.State != TournamentState.WaitingForPlayer)
        {
            await this._tournamentRepository.DeleteAsync(toDelete);
            return true;
        }

        return false;
    }
}