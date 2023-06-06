using Walmart.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Walmart.Web.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _context;
        public ShoppingCartViewComponent(IUnitOfWork context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity!.IsAuthenticated)
            {
                int count = 0;
                ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
                string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                var items = _context.ShoppingCarts.GetAll(p => p.UserId == userid);
                foreach (var item in items)
                {
                    count = count + item.Product_Quantity;
                }
                return View(count);
            }
            else
            {
                return View(0);
            }
        }
    }
}
