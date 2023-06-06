using Microsoft.AspNetCore.Identity;
using Walmart.ApplicationCore.Models;

namespace Walmart.ApplicationCore.DtoModels;

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string? Category_Name { get; set; }
    public string? Category_Image { get; set; }
    public ICollection<SubCategory>? SubCategories { get; set; }

}