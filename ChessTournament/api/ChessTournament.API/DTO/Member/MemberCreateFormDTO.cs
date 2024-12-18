using System.ComponentModel.DataAnnotations;
using ChessTournament.Domain.Enum;

namespace ChessTournament.API.DTO.Member;

public class MemberCreateFormDTO
{
    [Required] // plus besoin de cette annotation, 
    public string Username { get; set; } // string = requis, string? = non requis
    [Required]
    [EmailAddress] // possibilité de créer ses propres annotation en héritant de validationattribute et override la méthode isvalid
    public string Mail { get; set; }
    [Required]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    [Required]
    public DateTime Birthday { get; set; }
    [Required]
    [EnumDataType(typeof(Gender))]
    public Gender Gender { get; set; }

    public int? Elo { get; set; }
}
