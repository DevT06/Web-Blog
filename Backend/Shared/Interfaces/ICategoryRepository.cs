using Shared.Entities;
using Shared.Enum;

namespace Shared.Interfaces;

public interface ICategoryRepository
{
    Category? GetById(CategoryEnum id);

    List<Category> GetAll();

    Task<Category?> GetByIdAsync(CategoryEnum id);
}