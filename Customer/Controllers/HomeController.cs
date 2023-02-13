using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ApplicationDbContext.Models;

namespace Customer.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {

        }

        public IActionResult Index()
        {
            if (HttpContext.Session.Keys.Contains("UserId"))
            {
                var userId = HttpContext.Session.GetString("UserId");
                ViewBag.UserId = userId;

                var user = _userManager.FindByIdAsync(userId).Result;
                ViewBag.UserName = user.FirstName + " " + user.LastName;


                var roles =  _userManager.GetRolesAsync(user).Result;
                if (roles.Contains("Admin"))
                    ViewBag.HasDashboard = true;
            }
            TrendController trendCon = new TrendController(_uow, _mapper);
            var trends = trendCon.GetAll();
            return View(trends);
        }
    }
}
