using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.UOW;
using AutoMapper;

namespace Customer.Controllers
{
    public class WishListController : BaseController
    {
        public WishListController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {

        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Detail()
        {
            return View();

        }
    }
}
