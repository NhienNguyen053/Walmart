using Microsoft.AspNetCore.Identity;

namespace Walmart.ApplicationCore.Models;

public class Product
{
    public enum ProductStatus
    {
        Draft,
        Active,
        Sold
    }
    public int ProductId { get; set; }
    public int SubCategory_Id { get; set; }
    public string? SubCategory_Name { get; set; }
    public int Product_Number { get; set; }
    public string? Product_Name { get; set; }
    public string? Product_Description { get; set; }
    public decimal Product_Price { get; set; }
    public ProductStatus Product_Status { get; set; }
    public int Product_Quantity { get; set; }
    public SubCategory? SubCategory { get; set;}
    public List<ProductPhoto>? ProductPhotos { get; set; }
}