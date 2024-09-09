using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Entities;
using Shared.Enum;

namespace DataAccess.EFCore.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{


    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("CategoryEntries");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).HasColumnName("Id").ValueGeneratedNever();
        builder.Property(b => b.Name).HasColumnName("Name").IsUnicode().HasMaxLength(200).IsRequired();

        builder.HasData(new List<Category>
        {
            new Category { Id = CategoryEnum.Travel,Name = "Travel" },
            new Category { Id = CategoryEnum.Holiday, Name = "Holiday" },
            new Category { Id = CategoryEnum.Car, Name = "Car" },
            new Category { Id = CategoryEnum.Beauty, Name = "Beauty" }
        });
    }
}