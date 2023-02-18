using ApplicationDbContext.Models;
using ApplicationDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Controllers
{
    public class BaseController : Controller
    {
        protected IUnitOfWork _uow;

        protected readonly IMapper _mapper;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;
        protected readonly RoleManager<UserRole> _roleManager;
        public BaseController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            
        }

        protected void getUserFromSession()
        {
            if (HttpContext.Session.Keys.Contains("UserId"))
            {
                var userId = HttpContext.Session.GetString("UserId");
                ViewBag.UserId = userId;

                var user = _userManager.FindByIdAsync(userId).Result;
                ViewBag.UserName = user.FirstName + " " + user.LastName;

                ViewBag.SiteLink = "https://localhost:7068";
            }
        }
    }
}
