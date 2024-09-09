using Shared.Entities;
using Shared.Enum;
using Shared.Interfaces;

namespace DataAccess.EFCore.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly WebBlogDbContext _context;

    public CategoryRepository(WebBlogDbContext context)
    {
        _context = context;
    }

    public Category? GetById(CategoryEnum id)
    {
        return _context.CategoryEntries.Find(id);
    }

    public List<Category> GetAll()
    {
        return _context.CategoryEntries.ToList();
    }

}