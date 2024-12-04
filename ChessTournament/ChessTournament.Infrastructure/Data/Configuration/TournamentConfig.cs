using ChessTournament.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessTournament.Infrastructure.Data.Configuration;

public class TournamentConfig :IEntityTypeConfiguration<Tournament>
{
    public void Configure(EntityTypeBuilder<Tournament> builder)
    {
        builder.ToTable("Tournament");
        
        // field

        builder.Property(t => t.Id)
               .ValueGeneratedOnAdd();
        builder.Property(t => t.Name)
               .IsRequired();
        builder.Property(t => t.Place);
        builder.Property(t => t.PlayerMin)
               .IsRequired();
        builder.Property(t => t.PlayerMax)
               .IsRequired();
        builder.Property(t => t.EloMin);
        builder.Property(t => t.EloMax);
        builder.Property(t => t.State)
               .IsRequired();
        builder.Property(t => t.ActualRound)
               .IsRequired();
        builder.Property(t => t.WomenOnly)
               .IsRequired();
        builder.Property(t => t.RegistrationEndDate)
               .IsRequired();
        builder.Property(t => t.CreationDate)
               .IsRequired();
        builder.Property(t => t.UpdateDate)
               .IsRequired();
        
        // constraint
        builder.HasKey(t => t.Id);
        
        builder.ToTable(t => t.HasCheckConstraint("CK_PLAYER_MIN", "PLAYERMIN BETWEEN 0 AND 32"));
        builder.ToTable(t => t.HasCheckConstraint("CK_PLAYER_MAX", "PLAYERMAX BETWEEN 0 AND 32"));
        builder.ToTable(t => t.HasCheckConstraint("CK_ELO_MIN", "ELOMIN IS NULL OR (ELOMIN BETWEEN 0 AND 3000)"));
        builder.ToTable(t => t.HasCheckConstraint("CK_ELO_MAX", "ELOMAX IS NULL OR (ELOMAX BETWEEN 0 AND 3000)"));
    }
}