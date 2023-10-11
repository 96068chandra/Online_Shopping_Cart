using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models.Dtos;
using ShopOnlineApi.Etity;
using ShopOnlineApi.Extensions;
using ShopOnlineApi.Repositories.Contracts;

namespace ShopOnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository,IProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }
        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId)
        {
            try
            {
                var cartItems = await this.shoppingCartRepository.GetItems(userId);
                if(cartItems == null)
                {
                    return NoContent();
                }
                var products=await this.productRepository.GetItems();

                if(products == null)
                {
                    throw new Exception("No products exist in the system");
                }
                var cartItemsDto = cartItems.ConvertDto(products);
                return Ok(cartItemsDto);







            }
            catch (Exception ex )
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetItem(int id)
        {

            try
            {
                var cartItem=await this.shoppingCartRepository.GetItem(id);
                if(cartItem == null)
                {
                    return NoContent();

                }
                var product=await this.productRepository.GetItem(cartItem.ProductId);
                if(product == null)
                {
                    return NotFound();
                }
                var cartItemDto=cartItem.ConvertDto(product);
                return Ok(cartItemDto);


            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<CartItemDto>> AddItemToCart([FromBody] CartItemToAddDto cartItemToAdd)
        {
            try
            {
                var newCartItem = await this.shoppingCartRepository.AddItem(cartItemToAdd);
                if (newCartItem == null)
                {
                    return NoContent();
                }
                var product = await this.productRepository.GetItem(newCartItem.ProductId);
                if (product == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrive product(productId:({cartItemToAdd.ProductId}))");
                }
                var newCartItemDto = newCartItem.ConvertDto(product);
                return CreatedAtAction(nameof(GetItem), new { id = newCartItem.Id }, newCartItemDto);
            }


            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
