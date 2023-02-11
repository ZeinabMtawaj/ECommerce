using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ApplicationDbContext.Models;

namespace Ecommerce.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {
                
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
