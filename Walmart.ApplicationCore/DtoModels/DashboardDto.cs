using Microsoft.AspNetCore.Identity;
using Walmart.ApplicationCore.Models;

namespace Walmart.ApplicationCore.DtoModels;

public class DashboardDto
{
    public int Total_Order { get; set; }
    public int Pending_Order { get; set; }
    public int Paid_Order { get; set; }
    public int Shipping_Order { get; set; }
    public int Total_User { get; set; }
    public int Customer { get; set; }
    public int Manager { get; set; }
    public int Admin { get; set; }
    public int Total_Earnings { get; set; }
    public int Total_Category { get; set; }
    public int Total_Subcategory { get; set; }
    public int Total_Product { get; set; }
}