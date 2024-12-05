using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ChessTournament.Infrastructure.Data.Configuration;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");

        // field
        builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
               .IsRequired()
               .HasConversion<string>();

        // Constraint
        builder.HasKey(c => c.Id);

        builder.HasData(new List<Category>
        {
            new Category
            {
                Id = 1,
                Name = CategoryEnum.Junior
            },
            new Category
            {
                Id = 2,
                Name = CategoryEnum.Veteran
            },
            new Category
            {
                Id = 3,
                Name = CategoryEnum.Senior
            }
        });
    }
}