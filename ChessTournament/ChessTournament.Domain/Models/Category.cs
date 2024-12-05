using ChessTournament.Domain.Enum;

namespace ChessTournament.Domain.Models;

public class Category
{
    public int? Id { get; set; }
    public required CategoryEnum Name { get; set; }
    public List<Tournament> Tournaments { get; set; }
}