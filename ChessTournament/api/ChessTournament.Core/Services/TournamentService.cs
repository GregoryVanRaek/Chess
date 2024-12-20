﻿using ChessTournament.Applications.Interfaces.Repository;
using ChessTournament.Applications.Interfaces.Service;
using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Exception;
using ChessTournament.Domain.Models;

namespace ChessTournament.Applications.Services;

public class TournamentService :ITournamentService
{
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IMemberService _memberService;

    public TournamentService(ITournamentRepository tournamentRepository, IMemberService memberService)
    {
        this._tournamentRepository = tournamentRepository;
        this._memberService = memberService;
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
        try
        {
            if (AllGamePlayed(entity))
            {
                entity.ActualRound++;
                return this._tournamentRepository.UpdateAsync(entity);
            }

            throw new Exception("Not all game of this round has been played");
        }
        catch (Exception e)
        {
            throw new DBException("Error while update the tournament");
        }
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

    public async Task<int> AddPlayer(Tournament tournament, Member member)
    {
        try
        {
            if (_memberService.CheckOneParticipation(tournament, member))
                return await this._tournamentRepository.AddPlayers(tournament, member);

            return 0;

        }
        catch (Exception e)
        {
            throw new DBException("An error occured while adding a member to the tournament");
        }
    }

    public async Task<int> RemovePlayer(Tournament tournament, Member member)
    {
        try
        {
            if (tournament.Members.Contains(member))
                return await this._tournamentRepository.RemovePlayers(tournament, member);

            return 0;
        }
        catch (Exception e)
        {
            throw new DBException("An error occured while adding a member to the tournament");
        }
    }

    public async Task<Tournament> StartTournament(Tournament toStart)
    {
        try
        {
            toStart.ActualRound = 1;
            toStart.UpdateDate = DateTime.Today;
            toStart.State = TournamentState.InProgress;

            toStart.Games = this.GenerateGames(toStart);

            return await this._tournamentRepository.StartTournament(toStart);
        }
        catch (Exception e)
        {
            throw new DBException("An error occured while starting the tournament");
        }
    }

    public async Task<Tournament> UpdateResult(Game gameToUpdate)
    {
        try
        {
            return await this._tournamentRepository.UpdateGameResult(gameToUpdate);
        }
        catch (Exception e)
        {
            throw new DBException("An error occured while updating the game result");
        }
    }
    
    private List<Game> GenerateGames(Tournament tournament)
    {
        List<Game> games = new List<Game>();
        List<Game> games2 = new List<Game>();
        
        int numberOfPlayers = tournament.Members.Count;

        List<Member> players = tournament.Members;
        
        if (players.Count % 2 != 0)
            players.Add(new Member { Username = "Pause", Mail = null, Password = null, Birthday = DateTime.Now, Gender = Gender.Other});
        
        int numberOfRounds = numberOfPlayers - 1;

        for (int round = 0; round < numberOfRounds; round++)
        {
            for (int i = 0; i < numberOfPlayers / 2; i++)
            {
                Member player1 = players[i];
                Member player2 = players[numberOfPlayers - 1 - i];
                
                // Je ne génère pas de match s'il y a le joueur pause dans le lot
                if (player1.Username == "Pause" || player2.Username == "Pause")
                    continue;
                
                Game game = new Game
                {
                    Result = GameEnum.Unplayed,
                    Tournament = tournament,
                    GameMembers = new List<GameMember>
                    {
                        new GameMember
                        {
                            Member = player1,
                            Color = (round % 2 == 0) ? ColorEnum.White : ColorEnum.Black 
                        },
                        new GameMember
                        {
                            Member = player2,
                            Color = (round % 2 == 0) ? ColorEnum.Black : ColorEnum.White
                        }
                    }
                };
                
                Game game2 = new Game
                {
                    Result = GameEnum.Unplayed,
                    Tournament = tournament,
                    GameMembers = new List<GameMember>
                    {
                        new GameMember
                        {
                            Member = player2,
                            Color = (round % 2 == 0) ? ColorEnum.White : ColorEnum.Black 
                        },
                        new GameMember
                        {
                            Member = player1,
                            Color = (round % 2 == 0) ? ColorEnum.Black : ColorEnum.White
                        }
                    }
                };
                
                games.Add(game);
                games2.Add(game2);
            }
            
            // Après chaque tour chaque joueur bouge de 1 sauf le 1 qui reste fixe
            Member lastPlayer = players[numberOfPlayers - 1];
            for (int i = numberOfPlayers - 1; i > 1; i--)
            {
                players[i] = players[i - 1];
            }
            players[1] = lastPlayer;
        }
        
        games.AddRange(games2);

        int actualRound = 1;

        for (int i = 0; i < games.Count ; i+=2)
        {
            games[i].RoundNumber = actualRound;
            games[i+1].RoundNumber = actualRound;
            actualRound++;
        }
        
        return games;
    }
    
    private bool AllGamePlayed(Tournament entity)
    {
        bool gamePlayed = true;
        var games = entity.Games.Where(g => g.RoundNumber == entity.ActualRound);
        
        foreach(Game game in games)
        {
            if (game.Result == GameEnum.Unplayed)
            {
                gamePlayed = false;
                break;
            }
        }

        return gamePlayed;
    }
    
    public async Task<List<PlayerScore>> GetScore(Tournament tournament)
    {
        List<PlayerScore> playerStatsList = new List<PlayerScore>();
        
        foreach (Member member in tournament.Members)
        {
            playerStatsList.Add(new PlayerScore
            {
                Username = member.Username,
                GamesPlayed = 0,
                Wins = 0,
                Losses = 0,
                Ties = 0,
                Score = 0
            });
        }

        foreach (Game game in tournament.Games)
        {
            Member? player1 = game.GameMembers.FirstOrDefault(gm => gm.Color == ColorEnum.White)?.Member;
            Member? player2 = game.GameMembers.FirstOrDefault(gm => gm.Color == ColorEnum.Black)?.Member;

            if (player1 != null && player2 != null)
            {
                PlayerScore player1Stats = playerStatsList.First(ps => ps.Username == player1.Username);
                PlayerScore player2Stats = playerStatsList.First(ps => ps.Username == player2.Username);

                // calcul des matchs joués
                if (game.Result != GameEnum.Unplayed)
                {
                    player1Stats.GamesPlayed++;
                    player2Stats.GamesPlayed++;
                }

                // calcul des résultats
                if (game.Result == GameEnum.White) 
                {
                    player1Stats.Wins++;
                    player1Stats.Score += 1;
                    player2Stats.Losses++;
                }
                else if (game.Result == GameEnum.Black)
                {
                    player2Stats.Wins++;
                    player2Stats.Score += 1;
                    player1Stats.Losses++;
                }
                else if (game.Result == GameEnum.Tie) 
                {
                    player1Stats.Ties++;
                    player2Stats.Ties++;
                    player1Stats.Score += 0.5;
                    player2Stats.Score += 0.5;
                }
            }
        }
        
        return playerStatsList.OrderByDescending(ps => ps.Score).ToList();
    }
    
}