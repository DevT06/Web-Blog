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
	        new Category { Id = CategoryEnum.Travel, Name = "Travel", Description = "Exploring new destinations, cultures, and experiences around the world." },
	        new Category { Id = CategoryEnum.Holiday, Name = "Holiday", Description = "Special occasions and celebrations, often marked by leisure and joy." },
	        new Category { Id = CategoryEnum.Car, Name = "Car", Description = "Everything about cars, from models and features to maintenance and reviews." },
	        new Category { Id = CategoryEnum.Beauty, Name = "Beauty", Description = "Topics about skincare, makeup, fashion, and enhancing personal appearance." },
	        new Category { Id = CategoryEnum.Country, Name = "Country", Description = "Insights and details about nations, their cultures, and landmarks." },
	        new Category { Id = CategoryEnum.Hiking, Name = "Hiking", Description = "Adventures in nature, including trails, mountains, and outdoor exploration." },
	        new Category { Id = CategoryEnum.Sports, Name = "Sports", Description = "Activities and competitions that involve physical effort and skill." },
	        new Category { Id = CategoryEnum.Life, Name = "Life", Description = "Reflections and insights on everyday living and personal experiences." },
	        new Category { Id = CategoryEnum.Education, Name = "Education", Description = "Learning opportunities, academic subjects, and personal growth." },
	        new Category { Id = CategoryEnum.Achievement, Name = "Achievement", Description = "Milestones and accomplishments in personal or professional life." },
	        new Category { Id = CategoryEnum.Story, Name = "Story", Description = "Narratives and accounts of experiences, real or fictional." }

		});
    }
}