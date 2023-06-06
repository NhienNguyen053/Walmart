using Microsoft.AspNetCore.Identity;
using Walmart.ApplicationCore.Models;

namespace Walmart.ApplicationCore.DtoModels;

public class CartDetailsDto
{
    public int Product_Id { get; set; }
    public string? Product_Name { get; set; }
    public decimal Product_Price { get; set; }
    public int Product_Quantity { get; set; }
    public string? Image_Path { get; set; }
    public decimal Total { get; set; }
    public string? Cart_Status { get; set; }
}