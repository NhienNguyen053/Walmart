using Microsoft.AspNetCore.Identity;

namespace Walmart.ApplicationCore.Models;

public class ShoppingCart
{
    public string? UserId { get; set; }
    public int ProductId { get; set; }
    public int Product_Quantity { get; set; }
    public ApplicationUser? User { get; set; }
    public Product? Product { get; set; }
}