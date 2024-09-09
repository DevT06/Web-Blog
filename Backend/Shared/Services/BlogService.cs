using AutoMapper;
using Shared.DTOs;
using Shared.Entities;
using Shared.Enum;
using Shared.Interfaces;
using Shared.IServices;

namespace Shared.Services;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;

    private readonly ICategoryRepository _categoryRepository;

    public BlogService(IBlogRepository blogRepository, ICategoryRepository categoryRepository)
    {
        _blogRepository = blogRepository;
        _categoryRepository = categoryRepository;
    }

    public Blog? GetById(int id)
    {
        var blog = _blogRepository.GetById(id);
        if (blog == null)
        {
            return null;
        }
        if (blog.FkCategory != null)
        {
            blog.Category = _categoryRepository.GetById((CategoryEnum)blog.FkCategory);
        }

        return blog;
    }

    public List<Blog> GetAll()
    {
        var blogs = _blogRepository.GetAll().ToList();

        return blogs;
    }

    public List<Blog> GetBySearch(int? categoryId, string? searchTerm)
    {
        var blogs = _blogRepository.GetBySearch(categoryId, searchTerm);
        return blogs;

    }

    public Blog Add(BlogRequestDto blogRequestDto)
    {
        var blog = new Blog();

        blog.Title = blogRequestDto.Title;
        blog.Text = blogRequestDto.Text;
        blog.Author = blogRequestDto.Author;
        blog.CreatedAt = blogRequestDto.LastChangedAt;
        blog.FkCategory = (CategoryEnum)blogRequestDto.CategoryId;
        if (blogRequestDto.CategoryId != null) 
        {
            blog.Category = _categoryRepository.GetById((CategoryEnum)blogRequestDto.CategoryId);
        }
        // later remove ? (question marks)
        _blogRepository.Add(blog);

        return blog;
    }

    public void Update(int id, BlogRequestDto blogRequestDto)
    {
        var existingBlog = _blogRepository.GetById(id);

        if (!string.IsNullOrWhiteSpace(blogRequestDto.Title))
        {
            existingBlog.Title = blogRequestDto.Title;
        }

        existingBlog.Text = blogRequestDto.Text;


        if (!string.IsNullOrWhiteSpace(blogRequestDto.Author))
        {
            existingBlog.Author = blogRequestDto.Author;
        }

        existingBlog.EditedAt = blogRequestDto.LastChangedAt;

        if (blogRequestDto.CategoryId != null)
        {
            existingBlog.FkCategory = (CategoryEnum)blogRequestDto.CategoryId;
            existingBlog.Category = _categoryRepository.GetById((CategoryEnum)blogRequestDto.CategoryId);
        }

        _blogRepository.Update(existingBlog);

    }
}