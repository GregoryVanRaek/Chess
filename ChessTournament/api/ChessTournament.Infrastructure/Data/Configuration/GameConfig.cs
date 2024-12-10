using ChessTournament.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessTournament.Infrastructure.Data.Configuration;

public class GameConfig : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Game");
        
        // field
        builder.Property(g => g.Id)
               .ValueGeneratedOnAdd();

        builder.Property(g => g.RoundNumber)
               .IsRequired();

        builder.Property(g => g.Result)
               .IsRequired()
               .HasConversion<string>();

        // constraint
        builder.HasKey(g => g.Id);

        builder.HasMany(g => g.GameMembers)
            .WithOne(gm => gm.Game)
            .HasForeignKey(gm => gm.GameId);
    }
}