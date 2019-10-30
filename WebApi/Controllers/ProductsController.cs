using System.Collections.Generic;
using System.Linq;
using Assignment4;
using Assignment4.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetProducts(int id)
        {
            var service = new DataService();
            var product = service.GetProduct(id);

            if (product != null)
            {
                var pDTO = new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Category = new CategoryDTO {
                        Id = product.Category.Id,
                        Name = product.Category.Name,
                        Description = product.Category.Description
                    }
                };
                return Ok(pDTO);
            }

            return NotFound();
        }

        [HttpGet("category/{id}")]
        public ActionResult<List<ProductDTO>> GetProductsFromCategory(int id)
        {
            var service = new DataService();
            var products = service.GetProductByCategory(id);
            if (products != null)
            {
                var pDTO = products.Select(p => new ProductDTO {
                    Id = p.Id,
                    Name = p.Name,
                    Category = new CategoryDTO {
                        Id = p.Category.Id,
                        Name = p.Category.Name,
                        Description = p.Category.Description
                    }
                });
                return Ok(pDTO);
            }
            return NotFound(JsonConvert.SerializeObject(new List<ProductDTO>()));
        }

        [HttpGet("name/{substring}")]
        public ActionResult GetProductsFromSubstring(string substring)
        {
            var service = new DataService();
            var products = service.GetProductByName(substring);
            if (products != null && products.Count > 0)
            {
                var pDTO = products.Select(p => new ProductDTO {
                    Id = p.Id,
                    Name = p.Name
                });
                return Ok(pDTO);
            }

            return NotFound(JsonConvert.SerializeObject(new List<ProductDTO>()));
        }
    }
}