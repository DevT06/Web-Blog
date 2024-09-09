using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;
using Shared.Interfaces;
using Shared.IServices;
using Shared.Services;

namespace WebBlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryService categoryService, ICategoryRepository categoryRepository)
        {
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _categoryService.GetAll();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            var foundCategory = _categoryService.GetById(id);
            if (foundCategory == null)
            {
                return NotFound($"No Blog found with id: {id}");
            }


            return Ok(foundCategory);
        }
    }
}
