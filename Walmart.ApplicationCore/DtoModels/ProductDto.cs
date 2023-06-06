using Microsoft.AspNetCore.Identity;
using static Walmart.ApplicationCore.Models.Product;

namespace Walmart.ApplicationCore.DtoModels;

public class ProductDto
{
    public int ProductId { get; set; }
    public int Product_Number { get; set; }
    public string? SubCategory_Name { get; set; }
    public string? Product_Name { get; set; }
    public decimal Product_Price { get; set; }
    public ProductStatus Product_Status { get; set; }
    public int Product_Quantity { get; set; }

}