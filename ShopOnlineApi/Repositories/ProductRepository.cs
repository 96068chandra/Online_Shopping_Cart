using Microsoft.EntityFrameworkCore;
using ShopOnlineApi.Data;
using ShopOnlineApi.Entity;
using ShopOnlineApi.Repositories.Contracts;

namespace ShopOnlineApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext ShopOnlineDbContext;

        public ProductRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            ShopOnlineDbContext = shopOnlineDbContext;
        }

        

        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories =await this.ShopOnlineDbContext.ProductCategories.ToListAsync();
            return categories;

        }

        public Task<ProductCategory> GetCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products=await this.ShopOnlineDbContext.Products.ToListAsync();
            return products;



            
        }
    }
}
