using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Controllers

{
    public class AdminController : BaseController
    {

        public AdminController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {

        }

        public IActionResult SpecificationIndex()
        {
            return RedirectToAction("Index", "Specification");
        }

        public IActionResult CategoryIndex()
        {
            return RedirectToAction("Index", "Category");
        }

        public IActionResult ProductIndex()
        {
            return RedirectToAction("Index", "Product");
        }


    }
}
