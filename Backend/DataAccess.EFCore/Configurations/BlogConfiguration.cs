using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Entities;

namespace DataAccess.EFCore.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("BlogEntries");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasColumnName("Id").ValueGeneratedOnAdd();

        builder.Property(b => b.Title).HasColumnName("Title").IsUnicode().HasMaxLength(200).IsRequired();
        builder.Property(b => b.Text).HasColumnName("Text").IsUnicode().HasMaxLength(5000).IsRequired(false);
        builder.Property(b => b.Author).HasColumnName("Author").IsUnicode().HasMaxLength(200).IsRequired(true);

        builder.Property(b => b.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        builder.Property(b => b.EditedAt).HasColumnName("EditedAt").IsRequired(false);

        builder.HasOne(b => b.Category).WithMany(c => c.Blogs).HasForeignKey(b => b.FkCategory).IsRequired(false);
    }
}