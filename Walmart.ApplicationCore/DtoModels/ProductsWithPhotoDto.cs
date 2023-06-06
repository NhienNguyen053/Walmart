using Microsoft.AspNetCore.Identity;
using static Walmart.ApplicationCore.Models.Product;

namespace Walmart.ApplicationCore.DtoModels;

public class ProductsWithPhotoDto
{
    public int ProductId { get; set; }
    public string? Product_Name { get; set; }
    public decimal Product_Price { get; set; }
    public string? Product_Description{ get; set; }
    public int ProductPhotoId { get; set; }
    public string? Image_Path { get; set; }
}