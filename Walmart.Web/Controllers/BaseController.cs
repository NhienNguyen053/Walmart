using Microsoft.AspNetCore.Mvc;
using Walmart.Infrastructure.Interfaces;

namespace Walmart.Web.Controllers
{
    // This controller contains the uow class containing our WalmartContext. 
    public abstract class BaseController : Controller
    {
        protected readonly IUnitOfWork _uow;
        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
