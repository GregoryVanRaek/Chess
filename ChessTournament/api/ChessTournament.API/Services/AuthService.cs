using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChessTournament.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace ChessTournament.API.Services;

public class AuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(Member member)
    {
        // création d'un objet de sécurité avec les informations à stocker dans le token (pas d'informations sensibles)
        List<Claim> claims = new List<Claim>()
        {
            new Claim("Id", member.Id.ToString()),
            new Claim("Username", member.Username),
            new Claim("Elo", member.Elo.ToString()),
            new Claim("Email", member.Mail),
            new Claim("Role", member.Role.ToString()),
        };
        
        // clé de cryptage
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        // nécessaire pour ajouter une signature au token(connais la clé et l'algo de cryptage)
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        
        // Génération du token
        JwtSecurityToken token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"], // server qui génère l'api
            _configuration["Jwt:Audience"], // qui l'utilise
            claims,
            expires: DateTime.Now.AddDays(60), // A modifier pour faire un token de 15 minutes + un refreshtoken
            signingCredentials: creds
        );
        
        // export du token sous forme de string 
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}