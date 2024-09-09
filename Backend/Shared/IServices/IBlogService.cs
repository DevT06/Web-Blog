using Shared.DTOs;
using Shared.Entities;

namespace Shared.IServices;

public interface IBlogService
{
    Blog? GetById(int id);

    List<Blog> GetAll();

    List<Blog> GetBySearch(int? categoryId, string? searchTerm);

    Blog Add(BlogRequestDto blogRequestDto);

    void Update(int id, BlogRequestDto blogRequestDto);
}