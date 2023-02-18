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
    public class RatingController : BaseController
    {
        public RatingController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetRating(int productId)
        {
            getUserFromSession();

            if (ViewBag.UserId != null)
            {
                int userId = int.Parse(ViewBag.UserId);    
                Rating rating = _uow.RatingRepo.Find(x => ((x.ProductId == productId)&&(x.UserId == userId)));
                if (rating != null)
                {
                    return Json(rating.Rating1);
                }

            }
            

            return Json("0");

        }
        public JsonResult manageRating(int productId, int value)
        {
            getUserFromSession();

            if (ViewBag.UserId != null)
            {
                int userId = int.Parse(ViewBag.UserId);
                var rating = _uow.RatingRepo.Find(x => ((x.ProductId == productId) && (x.UserId == userId)));
                if (rating != null)
                {
                    int id = rating.Id;
                    var rate = new Rating()
                    {
                        Id = id,    
                        Rating1 = value,
                        UserId = userId,
                        ProductId = productId

                    };
                    _uow.RatingRepo.Update(rate);
                    _uow.SaveChanges();
                }
                else
                {
                    var rate = new Rating()
                    {
                        Rating1 = value,
                        UserId = userId,
                        ProductId = productId

                    };
                    _uow.RatingRepo.Add(rate);
                    _uow.SaveChanges();
                }

                return Json("true");


            }
            return Json("false");


        }


       
       

    }
}
