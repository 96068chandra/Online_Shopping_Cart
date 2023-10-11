using ShopOnlineApi.Entity;

namespace ShopOnlineApi.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetId(int id);
        Task<ProductCategory> GetCategoryId(int id);


    }
}
