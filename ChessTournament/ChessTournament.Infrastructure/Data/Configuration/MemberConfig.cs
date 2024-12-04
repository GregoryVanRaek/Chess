using ChessTournament.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChessTournament.Infrastructure.Data.Configuration;

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

    }
}