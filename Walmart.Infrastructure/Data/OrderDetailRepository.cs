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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {

    // private new WalmartContext _context;
        public OrderDetailRepository(WalmartContext context) : base(context)
        {
            // _context = context;
        }
    }
}