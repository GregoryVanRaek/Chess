using ChessTournament.API.DTO.Member;
using ChessTournament.API.Mappers;
using ChessTournament.API.Services;
using ChessTournament.Applications.Interfaces.Service;
using ChessTournament.Domain.Const;
using ChessTournament.Domain.Exception;
using ChessTournament.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ChessTournament.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;
    private readonly MailService _mailService;
    private readonly AuthService _authService;
    
    public MembersController(IMemberService memberService, MailService mailservice, AuthService authService)
    {
        this._memberService = memberService;
        this._mailService = mailservice;
        this._authService = authService;
    }
    
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MemberViewDTO>>> GetAll()
    {
        try
        {
            IEnumerable<Member> members = _memberService.GetAllAsync().ToBlockingEnumerable();

            if (!members.Any())
                return NotFound("No member found");

            IEnumerable<MemberViewDTO> memberDTOs = members.Select(MemberMapper.ToListDTO);
            
            return Ok(memberDTOs);
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

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MemberViewDTO>> GetById([FromRoute] int id)
    {
        try
        {
            Member? member = await this._memberService.GetOneByIdAsync(id);

            if (member is null)
                return NotFound("This member doesn't exist");

            return Ok(member.ToDTO());
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

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MemberViewDTO>> Create([FromBody] MemberCreateFormDTO memberDto)
    {
        if(memberDto is null || !ModelState.IsValid)
            return BadRequest("Invalid request for member creation");
        
        if (memberDto.Elo < MemberConst.MIN_ELO || memberDto.Elo > MemberConst.MAX_ELO)
            return BadRequest($"Elo must be between ${MemberConst.MIN_ELO} and ${MemberConst.MAX_ELO}");
        
        try
        {
            Member? toCreate = await this._memberService.CreateAsync(memberDto.ToMember());
            
            if (toCreate is null)
                return BadRequest("Impossible to create this member");
            
            this._mailService.SendWelcome(toCreate);
        
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
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Login([FromBody] MemberLoginDTO user)
    {
        Member? response = await _memberService.Login(user.Username, user.Password);
        
        if (response is not null)
        {
            string token = this._authService.GenerateToken(response);
            return Ok(token);
        }

        return BadRequest("Invalid credentials");
    }
    
}