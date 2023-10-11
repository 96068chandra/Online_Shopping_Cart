using ShopOnline.Models.Dtos;
using ShopOnlineApi.Entity;
using ShopOnlineApi.Etity;

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
        public static ProductDto ConvertDto(this Product product, ProductCategory productCategory)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Qty = product.Qty,
                CategoryId = product.CategoryId,
                ImageURL = product.ImageURL,
                CategoryName = productCategory.Name
                

            };
        }

        public static IEnumerable<CartItemDto> ConvertDto(this IEnumerable<CartItem> cartItems,IEnumerable<Product> products)
        {
            return (from item in cartItems
                    join product in products
                    on item.CartId equals product.Id
                    select new CartItemDto
                    {
                        Id=item.CartId,
                        ProductId=item.Id,
                        ProductName=product.Name,
                        ProductDescription=product.Description,
                        ProductImageURL=product.ImageURL,
                        Price  =product.Price,
                        Qty=item.Qty,
                        TotalPrice=item.Qty*product.Price,


                    }).ToList();
        }
        public static CartItemDto ConvertDto(this CartItem item,Product product)
        {
            return new CartItemDto
                    {
                        Id = item.CartId,
                        ProductId = item.Id,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        Qty = item.Qty,
                        TotalPrice = item.Qty * product.Price,


                    };
        }

    }
}
