using ChessTournament.API.DTO.Member;
using ChessTournament.API.Mappers;
using ChessTournament.API.Services;
using ChessTournament.Applications.Interfaces.Service;
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
    
    public MembersController(IMemberService memberService, MailService mailservice)
    {
        this._memberService = memberService;
        this._mailService = mailservice;
    }
    
    [HttpGet("List")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MemberViewDTO>>> GetAll()
    {
        List<Member> members = await this._memberService.GetAllAsync();
        
        if(members is null)
            return NotFound("No member found");
        
        return Ok(members.Select(MemberMapper.ToListDTO));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberViewDTO>> GetById([FromRoute] int id)
    {
        Member? member = await this._memberService.GetOneByIdAsync(id);

        if (member is null)
            return NotFound("This member doesn't exist");

        return Ok(member.ToDTO());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MemberViewDTO>> Create([FromBody] MemberCreateFormDTO memberDto)
    {
        if(memberDto is null || !ModelState.IsValid)
            return BadRequest("Invalid request for member creation");
        
        if (memberDto.Elo == 0)
            memberDto.Elo = 1200;

        if (memberDto.Elo < 0 || memberDto.Elo > 3000)
            return BadRequest("Elo must be between 0 and 3000");
        
        Member? toCreate;
        
        try
        {
            toCreate = await this._memberService.CreateAsync(memberDto.ToMember());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        if (toCreate is null)
            return BadRequest("Impossible to create this member");
        
        this._mailService.SendWelcome(toCreate);
        
        return CreatedAtAction(nameof(GetById), new { id = toCreate.Id }, toCreate.ToDTO());
    }
    
}