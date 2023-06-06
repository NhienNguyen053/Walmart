using Microsoft.AspNetCore.Identity;

namespace Walmart.ApplicationCore.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string? Category_Name { get; set; }
    public string? Category_Image { get; set; }
    public List<SubCategory>? Subcategories { get; set; }
}