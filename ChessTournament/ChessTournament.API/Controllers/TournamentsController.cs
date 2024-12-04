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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<TournamentViewDTO>>> GetAll()
    {
        List<Tournament> tournaments = await this._tournamentService.GetAllAsync();
        
        if(tournaments is null)
            return NotFound("No tournament found");
        
        return Ok(tournaments.Select(TournamentMapper.ToListDTO));
    }
    
}