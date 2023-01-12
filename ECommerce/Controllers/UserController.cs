using ApplicationDbContext.UOW;
using AutoMapper;
using Ecommerce.Controllers;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models;
using ECommerce.Models.ViewModels;

namespace ECommerce.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }


        [HttpGet]
        public IActionResult Register()
        {
            UserVM userVM = new UserVM();

            return View(userVM);
        }


        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
