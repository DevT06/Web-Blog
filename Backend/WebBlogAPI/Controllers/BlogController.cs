using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Entities;
using Shared.Interfaces;
using Shared.IServices;

namespace WebBlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogService blogService, IBlogRepository blogRepository)
        {
            _blogService = blogService;
            _blogRepository = blogRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Blog> Get(int id)
        {
            var foundBlog = _blogService.GetById(id);

            if (foundBlog == null)
            {
                return NotFound($"No Blog found with id: {id}");
            }

            foundBlog.Category.Blogs.Clear();

            return Ok(foundBlog);
        }

        [HttpGet]
        [Route("list")]
        public ActionResult<Blog> Get(int? categoryId, string? searchTerm)
        {
            var blogs = _blogService.GetBySearch(categoryId, searchTerm);

            blogs.ForEach(b => b.Category.Blogs.Clear());
            
            return Ok(blogs);
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<BlogRequestDto> Post([FromBody] BlogRequestDto blogRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(blogRequestDto);
            }

            var blog = _blogService.Add(blogRequestDto);

            return CreatedAtAction(nameof(Get), new { id = blog.Id }/*, blogRequestDto*/);
        }

        [HttpPut("update/{id}")]
        public ActionResult<BlogRequestDto> Put(int id, [FromBody] BlogRequestDto blogRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(blogRequestDto);
            }

            if (_blogRepository.Exists(id))
            {
                _blogService.Update(id, blogRequestDto);
            }
            else
            {
                //maybe change later so new Blog is created
                return NotFound("Blog with this id was not found");
            }

            return Ok("Blog updated");
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<Blog> Delete(int id)
        {
            if (_blogRepository.GetById(id) == null)
                return NotFound();

            _blogRepository.DeleteById(id);
            return Ok("Blog was deleted"); // noContent...?
        }
    }
}
