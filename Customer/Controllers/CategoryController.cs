using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Customer.Models;
/*using ECommerce.Models.ViewModels;
*/using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {

        }


        /*  public IActionResult Index()
          {
              return View();
          }*/

        [HttpGet]
        public JsonResult GetAll()
        {
            var items = _uow.CategoryRepo.GetAll();
            return Json(items);
        }



    }
}
