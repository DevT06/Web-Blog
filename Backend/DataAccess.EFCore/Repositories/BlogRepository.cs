using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using Shared.Enum;
using Shared.Interfaces;

namespace DataAccess.EFCore.Repositories;

public class BlogRepository : IBlogRepository
{
    private readonly WebBlogDbContext _context;

    public BlogRepository(WebBlogDbContext context)
    {
        _context = context;
    }

    public Blog? GetById(int id)
    {
        return _context.BlogEntries
            //.Include(b => b.Category)
            .Find(id);
        //.FirstOrDefault(b => b.Id == id);
    }

    public List<Blog> GetAll()
    {
        return _context.BlogEntries
        .Include(b => b.Category)
        .ToList();
    }

    public List<Blog> GetBySearch(int? categoryId, string searchTerm)
    {
        var query = _context.BlogEntries.AsQueryable();

        //deferred loading
        if (categoryId.HasValue && Enum.IsDefined(typeof(CategoryEnum), categoryId))
        {
            query = query.Where(blog => blog.FkCategory.Equals((CategoryEnum)categoryId));
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(blog => blog.Title.Contains(searchTerm) || blog.Author.Contains(searchTerm));
        }



        //here the load is effectively done
        return query.Include(b => b.Category).ToList();
    }

    public Blog Add(Blog blog)
    {
        if (blog.Category != null)
        {
            _context.CategoryEntries.Attach(blog.Category);
        }

        _context.BlogEntries.Add(blog);
        _context.SaveChanges();

        return blog;
    }

    public Blog Update(Blog blog)
    {
        if (blog.Category != null)
        {
            _context.CategoryEntries.Attach(blog.Category);
        }
        _context.BlogEntries.Update(blog);
        _context.SaveChanges();

        return blog;
    }

    public void DeleteById(int id)
    {
        var existingBlog = GetById(id);

        _context.Remove(existingBlog);
        _context.SaveChanges();
    }

    public bool Exists(int id)
    {
        return _context.BlogEntries.Any(b => b.Id == id);
    }
}