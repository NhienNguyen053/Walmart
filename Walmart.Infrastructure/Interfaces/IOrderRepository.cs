using Walmart.ApplicationCore.Models;
using Walmart.ApplicationCore.ViewModels;
using Walmart.ApplicationCore.ParamModels;
using Walmart.ApplicationCore.DtoModels;

namespace Walmart.Infrastructure.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<OrderVM> GetOrdersByPaging(Param request);
        Task<OrderVM> GetOrdersByPaging2(Param request, string status);
    }
}