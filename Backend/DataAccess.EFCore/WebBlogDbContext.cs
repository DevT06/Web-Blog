using DataAccess.EFCore.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Entities;

namespace DataAccess.EFCore;

public class WebBlogDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Blog> BlogEntries { get; set; }
    public DbSet<Category> CategoryEntries { get; set; }

    //public DbSet<Comment> CommentEntries;

    public WebBlogDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BlogConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("AWSBlogDb"))
            .EnableDetailedErrors();
    }
}