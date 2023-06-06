using Microsoft.AspNetCore.Identity;
using Walmart.ApplicationCore.Models;

namespace Walmart.ApplicationCore.DtoModels;

public class OrderDetailsDto
{
    public Order? order { get; set; }
    public IEnumerable<OrderDetail>? orderDetails { get; set; }
}