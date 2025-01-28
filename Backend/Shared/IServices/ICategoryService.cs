using Shared.Entities;
using Shared.Enum;
using WebBlogAPI.Dtos;

namespace Shared.IServices;

public interface ICategoryService
{
    Category? GetById(int id);

    List<Category> GetAll();

    Task<CategoryDto?> GetByIdAsync(int id);
}