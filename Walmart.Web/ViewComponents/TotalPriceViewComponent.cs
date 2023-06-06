using Walmart.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Walmart.Web.ViewComponents
{
    public class TotalPriceViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _context;
        public TotalPriceViewComponent(IUnitOfWork context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity!.IsAuthenticated)
            {
                decimal total = 0;
                ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
                string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                var items = _context.ShoppingCarts.GetAll(p => p.UserId == userid);
                foreach (var item in items)
                {
                    var product = _context.Products.Get(item.ProductId);
                    total = total + (product.Product_Price * item.Product_Quantity);
                }
                return View(total);
            }
            else
            {
                return View(0.00);
            }
        }
    }
}
