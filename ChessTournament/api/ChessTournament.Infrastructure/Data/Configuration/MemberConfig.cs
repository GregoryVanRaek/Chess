using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessTournament.Infrastructure.Data.Configuration; 

/* on est pas obligé de faire cette configuration, on pourrait se contenter
 d'utiliser les data annotations dans les classes d'entités*/

public class MemberConfig : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Member");

        // fieds
        builder.Property(m => m.Id)
               .ValueGeneratedOnAdd();
        
        builder.Property(m => m.Username)
               .IsRequired();
        builder.Property(m => m.Mail)
               .IsRequired();
        builder.Property(m => m.Password)
               .IsRequired();
        builder.Property(m => m.Birthday)
               .IsRequired();
        builder.Property(m => m.Gender)
               .IsRequired()
               .HasConversion<string>();
        builder.Property(m => m.Elo)
               .IsRequired();
        builder.Property(m => m.Role)
               .IsRequired()
               .HasConversion<string>();
        
        // constraint
        builder.HasKey(m => m.Id);
        
        builder.HasIndex(m => m.Username)
               .IsUnique();
        builder.HasIndex(m => m.Mail)
               .IsUnique();
        
        builder.ToTable(m => m.HasCheckConstraint("CK_ELO", "ELO BETWEEN 0 AND 3000"));
        
        builder.HasMany(m => m.GameMembers)
               .WithOne(gm => gm.Member)
               .HasForeignKey(gm => gm.MemberId);
        
        builder.HasData(new Member
        {
               Id = 1,
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