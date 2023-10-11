using ShopOnline.Models.Dtos;
using ShopOnlineApi.Entity;
using ShopOnlineApi.Etity;

namespace ShopOnlineApi.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> AddItem(CartItemToAddDto itemToAdd);
        Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto);
        Task<CartItem> DeleteItem(int id);
        Task<CartItem> GetItem(int id);
        Task<IEnumerable<CartItem>> GetItems(int userId);
    }
}
