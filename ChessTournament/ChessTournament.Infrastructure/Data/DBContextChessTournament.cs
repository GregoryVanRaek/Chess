using ChessTournament.Domain.Models;
using ChessTournament.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ChessTournament.Infrastructure.Data;

public class DbContextChessTournament : DbContext
{
    public DbContextChessTournament(DbContextOptions options) : base(options){}

    #region Entities
    public DbSet<Member> Members { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Category> Categories { get; set; }

    #endregion

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MemberConfig());
        modelBuilder.ApplyConfiguration(new TournamentConfig());
        modelBuilder.ApplyConfiguration(new CategoryConfig());
    }
}