using Microsoft.AspNetCore.Identity;
using Walmart.ApplicationCore.Models;

namespace Walmart.ApplicationCore.DtoModels;

public class UpdateOrderDto
{
    public decimal item_total { get; set; }
    public decimal total_price { get; set; }
}