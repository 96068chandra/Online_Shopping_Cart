using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models.Dtos;
using ShopOnlineApi.Extensions;
using ShopOnlineApi.Repositories.Contracts;

namespace ShopOnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                var products = await this.productRepository.GetItems();
                var productcategories=await this.productRepository.GetCategories();
                if (products == null || productcategories == null)
                {
                    return NotFound();
                }
                else
                {
                    var productDto = products.ConvertDto(productcategories);
                    return Ok(productDto);

                }
                

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");


            }

        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id)
        {
            try
            {
                var product = await this.productRepository.GetItem(id);
                
                if (product == null )
                {
                    return BadRequest();
                }
                else
                {
                    var productCategory = await this.productRepository.GetCategoryById(product.CategoryId);
                    var productDto=product.ConvertDto(productCategory);
                    
                    
                    return Ok(productDto);

                }


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");


            }

        }

    }
}
