using ChessTournament.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessTournament.Infrastructure.Data.Configuration;

public class GameMemberConfig : IEntityTypeConfiguration<GameMember>
{
    public void Configure(EntityTypeBuilder<GameMember> builder)
    {
        builder.ToTable("MM_Games_Players");

        builder.HasKey(gm => new { gm.MemberId, gm.GameId });

        builder.Property(gm => gm.Color)
               .IsRequired()
               .HasConversion<string>(); 

        builder.HasOne(gm => gm.Member)
            .WithMany(m => m.GameMembers)
            .HasForeignKey(gm => gm.MemberId);

        builder.HasOne(gm => gm.Game)
            .WithMany(g => g.GameMembers)
            .HasForeignKey(gm => gm.GameId);
    }
}