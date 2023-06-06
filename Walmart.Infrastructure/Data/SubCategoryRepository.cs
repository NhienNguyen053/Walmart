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
    public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
    {

    // private new WalmartContext _context;

        public SubCategoryRepository(WalmartContext context) : base(context)
        {
            // _context = context;
        }
        public SubCategory CreateSubCategory(int id){
            SubCategory subcategory = new SubCategory();
            subcategory.SubCategory_Name = "New SubCategory";
            subcategory.Category_Id = id;
            return subcategory;
        }
    }
}