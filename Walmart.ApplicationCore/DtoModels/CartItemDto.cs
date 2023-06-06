using Microsoft.AspNetCore.Identity;
using Walmart.ApplicationCore.Models;

namespace Walmart.ApplicationCore.DtoModels;

public class CartItemDto
{
    public int items_count { get; set; }
    public decimal total_price { get; set; }
}