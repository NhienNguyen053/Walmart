using Microsoft.AspNetCore.Identity;
using Walmart.ApplicationCore.Models;

namespace Walmart.ApplicationCore.DtoModels;

public class UpdateCartDto
{
    public int Product_Id { get; set; }
    public decimal Product_Price { get; set; }
    public int Product_Quantity { get; set; }
    public decimal Total { get; set; }
    public string? Cart_Status { get; set; }
}