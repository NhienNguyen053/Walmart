using Microsoft.AspNetCore.Identity;
using Walmart.ApplicationCore.Models;

namespace Walmart.ApplicationCore.DtoModels;

public class OrderDto
{
    public int OrderId { get; set; }
    public string? Customer_Id { get; set; }
    public string? Customer_Name { get; set; }
    public string? Phone_Number { get; set; }
    public string? Address { get; set; }
    public DateTime CreateDate { get; set; }
    public string? Order_Status { get; set; }
    public string? Payment_Method { get; set; }
    public decimal? Total_Price { get; set; }  
}