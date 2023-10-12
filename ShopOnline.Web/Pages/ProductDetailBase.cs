using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ProductDetailBase:ComponentBase
    {
        [Parameter]
        public int Id { get; set; }
        [Inject]
        public IProductServices ProductService { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        public ProductDto Product { get; set; }

        public string ErrorMessgae { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Product=await ProductService.GetItem(Id);
            }
            catch (Exception ex)
            {
                ErrorMessgae=ex.Message;
                
            }
        }

        protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDto);
                NavigationManager.NavigateTo("/ShoppingCart");

            }
            catch (Exception ex)
            {

                ErrorMessgae = ex.Message;

            }

        }




    }
}
