using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ApplicationDbContext.Models;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager) : base(uow, mapper, userManager, signInManager, roleManager)
        {
                
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
