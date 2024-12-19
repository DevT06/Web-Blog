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

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("Id").ValueGeneratedNever();
        
        builder.Property(c => c.Name).HasColumnName("Name").IsUnicode().HasMaxLength(200).IsRequired();
        builder.Property(c => c.Description).HasColumnName("Description").IsUnicode().HasMaxLength(5000).IsRequired();
        builder.Property(c => c.ImageId).HasColumnName("ImageId").IsRequired(false);

        builder.HasData(new List<Category>
        {
            new Category { Id = CategoryEnum.Travel,Name = "Travel" },
            new Category { Id = CategoryEnum.Holiday, Name = "Holiday" },
            new Category { Id = CategoryEnum.Car, Name = "Car" },
            new Category { Id = CategoryEnum.Beauty, Name = "Beauty" },
            new Category { Id = CategoryEnum.Country, Name = "Country" },
            new Category { Id = CategoryEnum.Hiking, Name = "Hiking" },
            new Category { Id = CategoryEnum.Sports, Name = "Sports" },
            new Category { Id = CategoryEnum.Life, Name = "Life" },
            new Category { Id = CategoryEnum.Education, Name = "Education" },
            new Category { Id = CategoryEnum.Achievement, Name = "Achievement" },
            new Category { Id = CategoryEnum.Story, Name = "Story" }
		});
    }
}