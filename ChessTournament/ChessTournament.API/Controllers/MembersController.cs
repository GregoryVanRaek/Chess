using ChessTournament.API.DTO.Member;
using ChessTournament.API.Mappers;
using ChessTournament.Applications.Interfaces.Service;
using ChessTournament.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChessTournament.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;
    
    public MembersController(IMemberService memberService)
    {
        this._memberService = memberService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MemberViewDTO>>> GetAll()
    {
        List<Member> members = await this._memberService.GetAllAsync();
        
        if(members is null)
            return NotFound("No member found");
        
        return Ok(members.Select(MemberMapper.ToListDTO));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberViewDTO>> GetById([FromRoute] int id)
    {
        Member? member = await this._memberService.GetOneByIdAsync(id);

        if (member is null)
            return NotFound("This member doesn't exist");

        return Ok(member.ToDTO());
    }
    
    
    
}