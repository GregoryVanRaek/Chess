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

    public IAsyncEnumerable<Tournament> GetAllAsync()
    {
        try
        {
            return _tournamentRepository.GetAllAsync();
        }
        catch (DBException e)
        {
            throw new DBException("Error while getting all tournament ");
        }
    }

    public Task<Tournament?> GetOneByIdAsync(int key)
    {
        try
        {
            return this._tournamentRepository.GetOneByIdAsync(key);
        }
        catch (DBException e)
        {
            throw new DBException("Error while getting the tournament ");
        }
    }
    
    public IEnumerable<Tournament> GetSomeTournament(int number)
    {
        try
        {
            return this._tournamentRepository.GetSomeTournament(number);
        }
        catch (DBException e)
        {
            throw new DBException("Error while getting the tournament ");
        }
    }


    public Task<Tournament> CreateAsync(Tournament entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Tournament> CreateAsync(Tournament entity, List<CategoryEnum> categoryEnums)
    {
        try
        {
            var categories = await _tournamentRepository.MapCategoriesAsync(categoryEnums);
            entity.Categories = categories;
            
            if(entity.PlayerMax < entity.PlayerMin )
                throw new WrongValueException("Player max can't be less than player min");
            
            if(entity.EloMin > entity.EloMax)
                throw new WrongValueException($"Elo max can't be less than Elo min");

            if (entity.CreationDate.AddDays(entity.PlayerMin) > entity.RegistrationEndDate)
                throw new WrongValueException($"The end date must be greater than the creation date + the number minimum of player");

            entity.State = TournamentState.WaitingForPlayer;
            entity.ActualRound = 0;
            entity.CreationDate = DateTime.Today;
            entity.UpdateDate = entity.CreationDate;
            
            return await this._tournamentRepository.CreateAsync(entity);
        }
        catch (DBException e)
        {
            throw new DBException("Error while creating a new tournament ");
        }
    }

    public Task<Tournament> UpdateAsync(Tournament entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Tournament entity)
    {
        Tournament toDelete = await GetOneByIdAsync((int)entity.Id);
        
        if (toDelete is not null && toDelete.State == TournamentState.WaitingForPlayer)
        {
            await this._tournamentRepository.DeleteAsync(toDelete);
            return true;
        }

        return false;
    }
}