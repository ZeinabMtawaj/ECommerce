using ApplicationDbContext.Models;
using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class BaseController : Controller
    {
        protected IUnitOfWork _uow;
        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}
