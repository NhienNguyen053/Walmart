using Microsoft.AspNetCore.Identity;
using Walmart.ApplicationCore.Models;
using static Walmart.ApplicationCore.Models.Product;

namespace Walmart.ApplicationCore.DtoModels;
public class EditProductDto
{
    public int ProductId { get; set; }
    public int Product_Number { get; set; }
    public int Category_Id { get; set; }
    public string? Category_Name { get; set; }
    public int SubCategory_Id { get; set; }
    public string? SubCategory_Name { get; set; }
    public string? Product_Name { get; set; }
    public decimal Product_Price { get; set; }
    public string? Product_Description { get; set; }
    public ProductStatus Product_Status { get; set; }
    public int Product_Quantity { get; set; }
    public ICollection<ProductPhoto>? ImagePaths { get; set; }
    public ICollection<Category>? Categories { get; set; }
    public ICollection<SubCategory>? SubCategories { get; set; }
    public bool ShowPhoto { get; set; }
}