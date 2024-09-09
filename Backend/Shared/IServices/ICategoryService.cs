using Shared.Entities;

namespace Shared.IServices;

public interface ICategoryService
{
    Category? GetById(int id);

    List<Category> GetAll();
}