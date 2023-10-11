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

        public async Task<ProductCategory> GetCategoryById(int id)
        {
            var category = await ShopOnlineDbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await ShopOnlineDbContext.Products.FindAsync(id);
            return product;

        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products=await this.ShopOnlineDbContext.Products.ToListAsync();
            return products;



            
        }
    }
}
