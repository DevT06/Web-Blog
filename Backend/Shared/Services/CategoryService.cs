using AutoMapper;
using Shared.DTOs;
using Shared.Entities;
using Shared.Enum;
using Shared.Interfaces;
using Shared.IServices;

namespace Shared.Services;

public class CategoryService : ICategoryService
{
    private readonly IBlogRepository _blogRepository;

    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(IBlogRepository blogRepository, ICategoryRepository categoryRepository)
    {
        _blogRepository = blogRepository;
        _categoryRepository = categoryRepository;
    }
    public Category? GetById(int id)
    {
        return _categoryRepository.GetById((CategoryEnum) id);
    }

    public List<Category> GetAll()
    {
        var categories = _categoryRepository.GetAll();
        return categories;
    }
}