using Walmart.ApplicationCore.Models;
using Walmart.ApplicationCore.ViewModels;
using Walmart.ApplicationCore.ParamModels;
using Walmart.ApplicationCore.DtoModels;

namespace Walmart.Infrastructure.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Product CreateProduct();
        Task<ProductVM> GetProductsByPaging(Param request);
        Task<ProductVM> GetProductsByPaging2(Param request, int categoryid);
        Task<ProductVM> GetProductsByPaging3(Param request, int subcategoryid);
        IEnumerable<ProductsWithPhotoDto> GetProductsWithPhoto();
    }
}