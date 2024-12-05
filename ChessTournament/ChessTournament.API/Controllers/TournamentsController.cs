using ChessTournament.API.DTO.Tournament;
using ChessTournament.API.Mappers;
using ChessTournament.API.Services;
using ChessTournament.Applications.Interfaces.Service;
using ChessTournament.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChessTournament.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TournamentsController : ControllerBase
{
    private readonly ITournamentService _tournamentService;
    private readonly MailService _mailService;
    
    public TournamentsController(ITournamentService tournamentService, MailService mailService)
    {
        this._tournamentService = tournamentService;
        this._mailService = mailService;
    }

    [HttpGet("AllTournaments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TournamentViewDTO>>> GetAll()
    {
        List<Tournament> tournaments = new List<Tournament>();
            
        await foreach (Tournament tournament in _tournamentService.GetAllAsync())
            tournaments.Add(tournament);

        if (!tournaments.Any())
            return NotFound("No tournament found");

        IEnumerable<TournamentViewDTO> tournamentDTOs = tournaments.Select(TournamentMapper.ToListDTO);

        return Ok(tournamentDTOs);
    }
    
}