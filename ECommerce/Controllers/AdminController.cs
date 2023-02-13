using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;
using AutoMapper;
using ApplicationDbContext.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Controllers

{
    public class AdminController : BaseController
    {

        public AdminController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }
        //public AdminController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        //{
        //}

        public IActionResult SpecificationIndex()
        {
            getUserFromSession();
            return RedirectToAction("Index", "Specification");
        }

        public IActionResult CategoryIndex()
        {
            getUserFromSession();
            return RedirectToAction("Index", "Category");
        }

        public IActionResult ProductIndex()
        {
            getUserFromSession();
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Dashboard()
        {
            getUserFromSession();
            return View();
        }

    }
}
