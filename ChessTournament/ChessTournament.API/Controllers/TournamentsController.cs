using ChessTournament.API.DTO.Tournament;
using ChessTournament.API.Mappers;
using ChessTournament.API.Services;
using ChessTournament.Applications.Interfaces.Service;
using ChessTournament.Domain.Const;
using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Exception;
using ChessTournament.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChessTournament.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TournamentsController : ControllerBase
{
    private readonly ITournamentService _tournamentService;
    private readonly IMemberService _memberService;
    private readonly MailService _mailService;
    
    public TournamentsController(ITournamentService tournamentService, MailService mailService, IMemberService memberService)
    {
        this._tournamentService = tournamentService;
        this._mailService = mailService;
        this._memberService = memberService;
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

        IEnumerable<TournamentViewDTO> tournamentDTOs = tournaments.Select(TournamentMapper.ToDTO);

        return Ok(tournamentDTOs);
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TournamentViewDTO>> GetById([FromRoute] int id)
    {
        try
        {
            Tournament? tournament = await this._tournamentService.GetOneByIdAsync(id);

            if (tournament is null)
                return NotFound("This tournament doesn't exist");

            return Ok(tournament.ToDTO());
        }
        catch (DBException e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("NewTournament")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TournamentViewDTO>> Create([FromBody] TournamentFormDTO tournamentDto)
    {
        if(tournamentDto is null || !ModelState.IsValid)
            return BadRequest("Invalid request for tournament creation");

        if (tournamentDto.PlayerMin < TournamentConst.MIN_PLAYER || tournamentDto.PlayerMax > TournamentConst.MAX_PLAYER 
            || tournamentDto.PlayerMax < TournamentConst.MIN_PLAYER || tournamentDto.PlayerMax > TournamentConst.MAX_PLAYER)
            return BadRequest($"Min and max players must be between {TournamentConst.MIN_PLAYER} and {TournamentConst.MAX_PLAYER}");

        if (tournamentDto.EloMin < TournamentConst.MIN_ELO || tournamentDto.EloMin > TournamentConst.MAX_ELO 
           || tournamentDto.EloMax <  TournamentConst.MIN_ELO || tournamentDto.EloMax > TournamentConst.MAX_ELO )
            return BadRequest($"Min and max Elo must be between {TournamentConst.MIN_ELO } and {TournamentConst.MAX_ELO }");
        
        try
        {
            Tournament? toCreate = await _tournamentService.CreateAsync(tournamentDto.ToTournament(), 
                tournamentDto.Categories.ToCategoryEnums());
            
            if (toCreate is null)
                return BadRequest("Impossible to create this tournament");

            IEnumerable<Member> participants = this._memberService.CheckParticipation(toCreate).ToBlockingEnumerable();
            
            foreach(Member m in participants)
                await _mailService.SendInvitation(m);
            
            return CreatedAtAction(nameof(GetById), new { id = toCreate.Id }, toCreate.ToDTO());
        }
        catch (DBException e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            Tournament toDelete = await this._tournamentService.GetOneByIdAsync(id);
            
            if(toDelete is null)
                return NotFound("This tournament doesn't exist");

            await this._tournamentService.DeleteAsync(toDelete);
            
            return NoContent();
        }
        catch (DBException e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}