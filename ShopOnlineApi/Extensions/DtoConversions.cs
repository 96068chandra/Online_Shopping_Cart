using ShopOnline.Models.Dtos;
using ShopOnlineApi.Entity;

namespace ShopOnlineApi.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductDto> ConvertDto(this IEnumerable<Product> products,IEnumerable<ProductCategory> categories)
        {
            return (from product in products
                   join productcategory in categories
                   on product.CategoryId equals productcategory.Id
                   select new ProductDto
                   {
                       Id=product.Id,
                       Name=product.Name,
                       Qty=product.Qty,
                       Price=product.Price,
                       Description=product.Description,
                       ImageURL=product.ImageURL,
                       CategoryId=product.CategoryId,
                       CategoryName=productcategory.Name

                   }).ToList();
        }



    }
}
