using ShopOnlineApi.Entity;

namespace ShopOnlineApi.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetItem(int id);
        Task<ProductCategory> GetCategoryById(int id);


    }
}
