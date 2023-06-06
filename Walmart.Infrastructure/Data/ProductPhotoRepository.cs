using Walmart.Infrastructure.Interfaces;
using Walmart.ApplicationCore.Models;
using Walmart.ApplicationCore.ViewModels;
using Walmart.ApplicationCore.ParamModels;
using Walmart.ApplicationCore.DtoModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

#nullable disable

namespace Walmart.Infrastructure.Data
{
    public class ProductPhotoRepository : Repository<ProductPhoto>, IProductPhotoRepository
    {

    // private new WalmartContext _context;

        public ProductPhotoRepository(WalmartContext context) : base(context)
        {
            // _context = context;
        }
        public ProductPhoto GetProductPhotoWithMaxSequence(int productId)
        {
            return _context.ProductPhotos
                .Where(p => p.Product_Id == productId)
                .OrderByDescending(p => p.Sequence)
                .FirstOrDefault();
        }
    }
}