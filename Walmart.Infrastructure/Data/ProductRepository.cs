using Walmart.Infrastructure.Interfaces;
using Walmart.ApplicationCore.Models;
using Walmart.ApplicationCore.ViewModels;
using Walmart.ApplicationCore.ParamModels;
using Walmart.ApplicationCore.DtoModels;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Walmart.Infrastructure.Data
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

    // private new WalmartContext _context;

        public ProductRepository(WalmartContext context) : base(context)
        {
            // _context = context;
        }
        public Product CreateProduct(){
            Product product = new Product();
            if (_dbSet.Count() > 0)
            {
                product.Product_Number = _dbSet.Max(p => p.Product_Number) + 1;
            }
            else
            {
                product.Product_Number = 100;
            }
            product.Product_Name = "New Product";
            product.Product_Status = Product.ProductStatus.Draft;
            SubCategory sub = (from subcategory in _context.SubCategories select subcategory).FirstOrDefault();
            product.SubCategory_Id = sub.SubCategoryId;
            product.SubCategory_Name = sub.SubCategory_Name;
            return product;
        }
        public async Task<ProductVM> GetProductsByPaging(Param request){
            var products = (from product in _context.Products
            select new ProductDto
            {
                ProductId = product.ProductId,
                Product_Number = product.Product_Number,
                SubCategory_Name = product.SubCategory_Name,
                Product_Name = product.Product_Name,
                Product_Price = product.Product_Price,
                Product_Status = product.Product_Status,
                Product_Quantity = product.Product_Quantity
            });
            var total = await products.CountAsync();
            var data = await products.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            return new ProductVM
            {
                DataList = data,
                Total = total
            };
        }
        public async Task<ProductVM> GetProductsByPaging2(Param request, int categoryid){
            var products = (from product in _context.Products
            join subcategory in _context.SubCategories on product.SubCategory_Id equals subcategory.SubCategoryId
            where subcategory.Category_Id == categoryid
            select new ProductDto
            {
                ProductId = product.ProductId,
                Product_Number = product.Product_Number,
                SubCategory_Name = product.SubCategory_Name,
                Product_Name = product.Product_Name,
                Product_Price = product.Product_Price,
                Product_Status = product.Product_Status,
                Product_Quantity = product.Product_Quantity
            });
            var total = await products.CountAsync();
            var data = await products.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            return new ProductVM
            {
                DataList = data,
                Total = total
            };
        }
        public async Task<ProductVM> GetProductsByPaging3(Param request, int subcategoryid){
            var products = (from product in _context.Products where product.SubCategory_Id == subcategoryid
            select new ProductDto
            {
                ProductId = product.ProductId,
                Product_Number = product.Product_Number,
                SubCategory_Name = product.SubCategory_Name,
                Product_Name = product.Product_Name,
                Product_Price = product.Product_Price,
                Product_Status = product.Product_Status,
                Product_Quantity = product.Product_Quantity
            });
            var total = await products.CountAsync();
            var data = await products.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            return new ProductVM
            {
                DataList = data,
                Total = total
            };
        }
public IEnumerable<ProductsWithPhotoDto> GetProductsWithPhoto()
{
    var products = (from product in _context.Products
                    join photos in _context.ProductPhotos on product.ProductId equals photos.Product_Id
                    where product.Product_Status == Product.ProductStatus.Active
                    group photos by product into grouped
                    select new ProductsWithPhotoDto
                    {
                        ProductId = grouped.Key.ProductId,
                        Product_Name = grouped.Key.Product_Name,
                        Product_Price = grouped.Key.Product_Price,
                        Product_Description = grouped.Key.Product_Description,
                        ProductPhotoId = grouped.First().ProductPhotoId,
                        Image_Path = grouped.First().Image_Path
                    }).Take(6);

    return products.ToList();
}
    }
}