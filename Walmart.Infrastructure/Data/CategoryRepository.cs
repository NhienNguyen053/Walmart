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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

    // private new WalmartContext _context;

        public CategoryRepository(WalmartContext context) : base(context)
        {
            // _context = context;
        }
        public Category CreateCategory(){
            Category category = new Category();
            category.Category_Name = "New Category";
            return category;

        }
        public List<CategoryDto> GetCategories(){
            var categories = (from category in _context.Categories
                            join SubCategory in _context.SubCategories on category.CategoryId equals SubCategory.Category_Id into subcategories
                            select new CategoryDto
                            {
                                CategoryId = category.CategoryId,
                                Category_Name = category.Category_Name,
                                Category_Image = category.Category_Image,
                                SubCategories = subcategories.Select(s => new SubCategory{
                                    SubCategoryId = s.SubCategoryId,
                                    Category_Id = s.Category_Id,
                                    SubCategory_Name = s.SubCategory_Name
                                }).ToList()
                            });
            return categories.ToList();
        }
    }
}