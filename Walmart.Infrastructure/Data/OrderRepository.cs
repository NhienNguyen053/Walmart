using Walmart.Infrastructure.Interfaces;
using Walmart.ApplicationCore.Models;
using Walmart.ApplicationCore.ViewModels;
using Walmart.ApplicationCore.ParamModels;
using Walmart.ApplicationCore.DtoModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

#nullable disable

namespace Walmart.Infrastructure.Data
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

    // private new WalmartContext _context;
        public OrderRepository(WalmartContext context) : base(context)
        {
            // _context = context;
        }
        public async Task<OrderVM> GetOrdersByPaging(Param request){
            var orders = (from order in _context.Orders
            select new OrderDto
            {
                OrderId = order.OrderId,
                Customer_Id = order.Customer_Id,
                Customer_Name = order.Customer_Name,
                Phone_Number = order.Phone_Number,
                Address = order.Address,
                CreateDate = order.CreateDate,
                Order_Status = order.Order_Status,
                Payment_Method = order.Payment_Method,
                Total_Price = order.Total_Price
            });
            orders = orders.OrderByDescending(p => p.CreateDate);
            var total = await orders.CountAsync();
            var data = await orders.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            return new OrderVM
            {
                DataList = data,
                Total = total
            };
        }
        public async Task<OrderVM> GetOrdersByPaging2(Param request, string status){
            var orders = (from order in _context.Orders where order.Order_Status == status
            select new OrderDto
            {
                OrderId = order.OrderId,
                Customer_Id = order.Customer_Id,
                Customer_Name = order.Customer_Name,
                Phone_Number = order.Phone_Number,
                Address = order.Address,
                CreateDate = order.CreateDate,
                Order_Status = order.Order_Status,
                Payment_Method = order.Payment_Method,
                Total_Price = order.Total_Price
            });
            orders = orders.OrderByDescending(p => p.CreateDate);
            var total = await orders.CountAsync();
            var data = await orders.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
            return new OrderVM
            {
                DataList = data,
                Total = total
            };
        }
    }
}