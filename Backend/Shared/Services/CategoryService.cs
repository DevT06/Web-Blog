using AutoMapper;
using Shared.DTOs;
using Shared.Entities;
using Shared.Enum;
using Shared.Interfaces;
using Shared.IServices;
using WebBlogAPI.Dtos;

namespace Shared.Services;

public class CategoryService : ICategoryService
{
	private readonly IS3Service _s3Service;

	private readonly IBlogRepository _blogRepository;

    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(IBlogRepository blogRepository, ICategoryRepository categoryRepository, IS3Service s3Service)
    {
        _blogRepository = blogRepository;
        _categoryRepository = categoryRepository;
        _s3Service = s3Service;
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

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
	    var category = await _categoryRepository.GetByIdAsync((CategoryEnum) id);
	    if (category == null)
	    {
		    return null;
	    }

	    var imageBase64 = await _s3Service.GetImageBase64Async("web-blog-bucket", category.ImageId.ToString() + ".jpg");
	    var categoryDto = new CategoryDto
	    {
		    Id = (int) category.Id,
		    Name = category.Name,
		    ImageId = category.ImageId,
		    ImageData = $"data:image/jpg;base64,{imageBase64}"
	    };

	    return categoryDto;
    }
}