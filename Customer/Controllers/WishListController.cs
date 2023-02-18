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
            getUserFromSession();
            var products = new List<Product>();
            if (ViewBag.UserId != null)
            {
                var userCon = new UserController(_uow, _mapper, _userManager, _signInManager);
                string? userId = ViewBag.UserId;
                var wishes = userCon.getWishList(userId);
                foreach (var wish in wishes)
                {
                    var product = _uow.ProductRepo.Find(x => x.Id == wish.ProductId);
                    products.Add(product);
                }
            }
            return View(products);
        }

        public IActionResult Detail()
        {
            

            return View();

        }


        public JsonResult Manage( int productId, string subtitle)
        {
            getUserFromSession();
            var product = _uow.ProductRepo.Find(x=> x.Id == productId);
            ViewBag.Subtitle = subtitle;
            if (product == null) {
               /* TempData["error"] = "This Product is Not Available!";*/
                return Json("false");
            }
            int userId = int.Parse(ViewBag.UserId);
            WishList wishList = new WishList() {
                ProductId = productId,
                UserId = userId,
                Quantity = 1
            };
            var wish = _uow.WishListRepo.Find(x => ((x.UserId == userId) && (x.ProductId == productId)));   
            if (wish != null)
            {
                this.Delete(wish);
               return Json("Delete");

            }
            this.Create(wishList);
           
          
            return Json("Add");




        }

        public void Create(WishList wish)
        {

            _uow.WishListRepo.Add(wish);
            _uow.SaveChanges();



        }

        public void Delete(WishList wish)
        {

            _uow.WishListRepo.Delete(wish);
            _uow.SaveChanges();



        }
        public JsonResult DeleteId(int id)
        {
            getUserFromSession();
            int userId = int.Parse(ViewBag.UserId);
            var wish = _uow.WishListRepo.Find(x=>((x.ProductId == id) && (x.UserId==userId)));
            _uow.WishListRepo.Delete(wish);
            _uow.SaveChanges();
            return Json("True");
        }
        public JsonResult DeleteAll()
        {
            getUserFromSession();
            int userId = int.Parse(ViewBag.UserId);
            var wishes = _uow.WishListRepo.FindAll(x =>  (x.UserId == userId));
            foreach (var wish in wishes)
            {
                _uow.WishListRepo.Delete(wish);
            }
            _uow.SaveChanges();
            return Json("True");
        }
    }
}
