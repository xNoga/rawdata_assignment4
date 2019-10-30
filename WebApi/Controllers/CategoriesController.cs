using System.Collections.Generic;
using System.Linq;
using Assignment4;
using Assignment4.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<CategoryDTO> GetCategories()
        {
            var service = new DataService();
            var cats = service.GetCategories();

            if (cats != null)
            {
                var convertedCats = cats.Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                });
                return Ok(convertedCats);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryDTO> GetCategory(int id)
        {
            var service = new DataService();
            var cat = service.GetCategory(id);
            if (cat != null) return Ok(convertCategory(cat));

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Category> CreateCategory([FromBody] Category cat)
        {
            var service = new DataService();
            var res = service.CreateCategory(cat.Name, cat.Description);
            if (res != null) return Created("/api/categories", convertCategory(res));
            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, [FromBody] Category cat)
        {
            var service = new DataService();
            var res = service.UpdateCategory(id, cat.Name, cat.Description);
            if (res) return Ok();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            var service = new DataService();
            var res = service.DeleteCategory(id);
            if (res) return Ok();

            return NotFound();
        }

        public CategoryDTO convertCategory(Category cat)
        {
            return new CategoryDTO
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description
            };
        }
    }
}