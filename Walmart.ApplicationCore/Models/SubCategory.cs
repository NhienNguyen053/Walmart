using Microsoft.AspNetCore.Identity;

namespace Walmart.ApplicationCore.Models;

public class SubCategory
{
    public int SubCategoryId { get; set; }
    public int Category_Id { get; set; }
    public string? SubCategory_Name { get; set; }
    public Category? Category { get; set; }
    public List<Product>? Products { get; set; }
}