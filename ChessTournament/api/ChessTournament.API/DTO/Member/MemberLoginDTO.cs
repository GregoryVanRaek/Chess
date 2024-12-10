using System.ComponentModel.DataAnnotations;

namespace ChessTournament.API.DTO.Member;

public class MemberLoginDTO
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}