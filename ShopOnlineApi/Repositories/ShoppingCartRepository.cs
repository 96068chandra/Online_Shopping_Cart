using Microsoft.EntityFrameworkCore;
using ShopOnline.Models.Dtos;
using ShopOnlineApi.Data;
using ShopOnlineApi.Etity;
using ShopOnlineApi.Repositories.Contracts;

namespace ShopOnlineApi.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext shopOnlineDbContext;

        public ShoppingCartRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            this.shopOnlineDbContext = shopOnlineDbContext;
        }

        private async Task<bool> CartItemExist(int cartId,int productItem)
        {
            return await this.shopOnlineDbContext.CartItems.AnyAsync(c=>c.CartId==cartId &&
                                                                        c.ProductId==productItem);

        }
        public async Task<CartItem> AddItem(CartItemToAddDto itemToAdd)
        {

            if(await CartItemExist(itemToAdd.CartId, itemToAdd.ProductId)==false)
            {
                var item = await (from product in this.shopOnlineDbContext.Products
                                  where product.Id == itemToAdd.ProductId
                                  select new CartItem
                                  {
                                      CartId = itemToAdd.CartId,
                                      ProductId = product.Id,
                                      Qty = itemToAdd.Qty,

                                  }).SingleOrDefaultAsync();
                if (item != null)
                {
                    var result = await this.shopOnlineDbContext.CartItems.AddAsync(item);
                    await this.shopOnlineDbContext.SaveChangesAsync();
                    return result.Entity;
                }

            }
            
            return null;

        }


        public Task<CartItem> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in this.shopOnlineDbContext.Carts
                          join cartItem in this.shopOnlineDbContext.CartItems
                          on cart.Id equals cartItem.Id
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              CartId = cartItem.CartId,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,


                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
       {
            return await (from cart in this.shopOnlineDbContext.Carts
                          join CartItem in this.shopOnlineDbContext.CartItems
                          on cart.Id equals CartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id=cart.Id,
                              ProductId=CartItem.ProductId,
                              Qty=CartItem.Qty,
                              CartId=CartItem.CartId,

                          }).ToListAsync();
        }

        public Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
