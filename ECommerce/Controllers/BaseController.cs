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
        public BaseController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public BaseController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        protected void getUserFromSession()
        {
            if (HttpContext.Session.Keys.Contains("UserId"))
            {
                var userId = HttpContext.Session.GetString("UserId");
                ViewBag.UserId = userId;

                var user = _userManager.FindByIdAsync(userId).Result;
                ViewBag.UserName = user.FirstName + " " + user.LastName;


                var roles = _userManager.GetRolesAsync(user).Result;
                if (roles.Contains("Admin"))
                    ViewBag.HasDashboard = true;
            }
        }
    }
}
