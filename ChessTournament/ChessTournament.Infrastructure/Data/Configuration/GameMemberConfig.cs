using ChessTournament.Domain.Enum;
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

        builder.HasData(new Member
        {
            Username = "Checkmate",
            Mail = "checkmate@chesstournament.com",
            Password =
                "$argon2id$v=19$m=65536,t=3,p=1$PNbprKT2GkhZC3XDv1cIkg$yF4B0x/45Wu887sTldoZEQYuRnmxWOp1YSx42KZNjrQ",
            Birthday = new DateTime(1993, 02, 14),
            Gender = Gender.Male,
            Elo = 3000,
            Role = Role.Admin
        });

    }
}