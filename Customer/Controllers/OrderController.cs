using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Customer.Models;
using Customer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;


namespace Customer.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {

        }

        public IActionResult Index()
        {
            getUserFromSession();

            /*            ProductController cont = new ProductController(_uow, _mapper);
                        var trends = cont.GetAll();*/
            var lambdas = new List<Expression<Func<Product, object>>>();
            lambdas.Add(x => x.Category);
            var prodList = _uow.ProductRepo.FindAll(x=>x.Id>=1,null, null, lambdas);   
            return View(prodList);  
        }

        public IActionResult Detail()
        {
            return View();

        }
    }
}
