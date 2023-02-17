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
    public class TrendController : BaseController
    {
        public TrendController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {

        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IEnumerable<Product> GetAll(string? userId)
        {
            //getUserFromSession();
            var items = _uow.TrendRepo.FindAll(x => x.Id >=1, 9, 0);
            ProductController cont = new ProductController(_uow, _mapper, _userManager, _signInManager);
            var trends = new List<Product>();
            var i = 0;
            Product trend;
            string name = "";
            IEnumerable<WishList> wishes = null;
            if (userId != null)
            {
                UserController userCont = new UserController(_uow, _mapper, _userManager, _signInManager);
                wishes = userCont.getWishList(userId);
            }
            foreach (var item in items)
            {
                /*if (i == 9)
                {
                    break;
                }*/

                trend = cont.getCategory(item.ProductId);
                if ((userId != null) && (wishes != null))
                {
                    if (wishes.Any(x => ((x.UserId == int.Parse(userId))&&(x.ProductId == trend.Id))))
                    {
                        trend.Sku = "wish";
                    }
                }


               /* if (trend.Category != null)
                {
                    name = trend.Category.Name;
                }
                trend.Category = null;*/


            /*    var viewObj = _mapper.Map<ProductViewModel>(trend);
                viewObj.IsTrend = name;*/




/*
                trend.Sku = name;   */

                trends.Add(trend);

                i++;
                
            }
            return trends;
        }

    }
}
