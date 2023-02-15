using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Customer.Models;
/*using ECommerce.Models.ViewModels;
*/
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;


namespace Customer.Controllers
{
    public class WishListController : BaseController
    {
        public WishListController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
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


        public JsonResult Create( int productId)
        {
            getUserFromSession();
            var product = _uow.ProductRepo.Find(x=>x.Id== productId);
            if (product == null) {
                return Json("false");
            }
            var userId = ViewBag.UserId;
            WishList wishList = new WishList() { 
                ProductId = productId, 
                UserId = userId
            };
            
            return Json("true");




        }
    }
}
