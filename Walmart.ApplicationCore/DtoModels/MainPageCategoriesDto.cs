using Microsoft.AspNetCore.Identity;
using Walmart.ApplicationCore.Models;

namespace Walmart.ApplicationCore.DtoModels;

public class MainPageCategoriesDto
{
    public int CategoryId { get; set; }
    public string? Category_Name { get; set; }
    public string? Category_Image { get; set; }
    public IEnumerable<SubCategory>? SubCategories { get; set; }
}