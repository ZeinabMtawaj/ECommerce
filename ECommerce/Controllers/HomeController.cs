using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.UOW;


namespace Ecommerce.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork uow):base(uow)
        {
                
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
