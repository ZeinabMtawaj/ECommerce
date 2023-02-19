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
using Microsoft.AspNetCore.Authorization;

namespace Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {

        }




        
        public Product getCategory( int id)
        {
            var lambdas = new List<Expression<Func<Product, object>>>();
            lambdas.Add(x => x.Category);
            var item = _uow.ProductRepo.Find(x => x.Id == id, lambdas);

            return item;
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            getUserFromSession();
          
            var lambdas = new List<Expression<Func<Product, object>>>();
            lambdas.Add(x => x.ProductSpecificationValues);
            lambdas.Add(x => x.Photos);
            lambdas.Add(x => x.Ratings);
            lambdas.Add(x => x.Category);
            Product product_detail = null;
            product_detail = _uow.ProductRepo.Find(x => x.Id == id, lambdas);
            var i = 0;
            Specification spec;
/*
            var lambdas_spec = new List<Expression<Func<ProductSpecificationValue, object>>>();
            lambdas_spec.Add(x => x.Specification);
            var lambdas_prod = new List<Expression<Func<Product, object>>>();
            lambdas_prod.Add(x => x.ProductSpecificationValues);
             
*/           if (product_detail == null)
            {
                this.Index("","all");
            }
            string name = "";
            if (product_detail.ProductSpecificationValues != null)
            {
                foreach (var item in product_detail.ProductSpecificationValues)
                {/*
                    name = "";
                    var prods = _uow.ProductRepo.FindAll(x => ((x.Name == product_detail.Name) && (x.ProductSpecificationValues !=null)), null, null, lambdas_prod);
*/
                    spec = _uow.SpecificationRepo.Find(x => x.Id == item.SpecificationId);
                    /*foreach (var prod in prods) {
                        var same_product = _uow.ProductSpecificationValueRepo.Find(x => (
                        (x.Specification != null) && (x.Specification.Name == spec.Name) && (x.ProductId == prod.Id))
                        , lambdas_spec);
                        if (same_product != null)
                        {
                            name += same_product.Value + "~";
                        }
                        
                    }

                    name = name.Remove(name.Length - 1, 1);*/
                    product_detail.ProductSpecificationValues.ElementAt(i).Specification = new Specification
                    {
                        Name =spec.Name
                    };
                    i++;

/*                    product_detail.ProductSpecificationValues.ElementAt(i).Value = name;
*/
                }
            }
            if (ViewBag.UserId != null)
            {
                var userCon = new UserController(_uow, _mapper, _userManager, _signInManager);
                string? userId = ViewBag.UserId;
                var wishes = (userCon.getWishList(userId));
                if (wishes.Any(x => x.ProductId == id))
                {
                    product_detail.Sku = "wish";
                }

            }
            return View(product_detail);  
        
        
        }
       

        public IActionResult Index(string match, string exp) {
            getUserFromSession();
            UserController userCon = new UserController(_uow, _mapper, _userManager, _signInManager);
            string? userId = ViewBag.UserId;
            IEnumerable<WishList> wishes = null;
            if (userId != null)
            {
                wishes = userCon.getWishList(userId);
            }

            var lambdas = new List<Expression<Func<Product, object>>>();
            lambdas.Add(x => x.Category);
            IEnumerable<Product> products = null;
            int take = 20;
            if (exp == "all")
            {
                products = _uow.ProductRepo.FindAll(x => x.Id >= 1, take, 0, lambdas);    
                ViewBag.subtitle = "All Products";
            }
           else if (exp == "category")
            {
                products = _uow.ProductRepo.FindAll(x => (x.Category.Name) == match, take, 0, lambdas);
                ViewBag.subtitle = "Products in "+match+" Category";


            }
            else
            {
                var z = Request.Form["match"];
                var e = 0;
                products = _uow.ProductRepo.FindAll(x => (((x.Name).Contains(z))|| ((x.Description).Contains(z))) , take, 0, lambdas);
                ViewBag.subtitle = "Searching for "+match+" ...";


            }
            ViewBag.exp = exp;
            ViewBag.match = match;
            if ((wishes != null)&&(userId != null))
            {
                var prods = products.Where(x => wishes.Any(y => ((y.ProductId == x.Id) && (y.UserId == int.Parse(userId)))));
                prods.ToList().ForEach(x => x.Sku ="wish");
            }
            return View(products);
        
        }


       

        public JsonResult getProducts(string match, string exp,int take, int skip) {
            getUserFromSession();
            UserController userCon = new UserController(_uow, _mapper, _userManager, _signInManager);
            string? userId = ViewBag.UserId;
            IEnumerable<WishList> wishes = null;
            if (userId != null)
            {
                wishes = userCon.getWishList(userId);
            }

            var lambdas = new List<Expression<Func<Product, object>>>();
            lambdas.Add(x => x.Category);
            IEnumerable<Product> products = null;
            if (exp == "all")
            {
                products = _uow.ProductRepo.FindAll(x => x.Id >= 1, take, skip, lambdas);
                ViewBag.subtitle = "All Products";


            }
            else if (exp == "category")
            {
                products = _uow.ProductRepo.FindAll(x => (x.Category.Name) == match, take, skip, lambdas);
                ViewBag.subtitle = "Products in " + match + " Category";


            }
            else
            {
              
                products = _uow.ProductRepo.FindAll(x => (((x.Name).Contains(match)) || ((x.Description).Contains(match))), take, skip, lambdas);
                ViewBag.subtitle = "Searching for " + match + " ...";


            }
            products.ToList().ForEach(x => x.Category = null);
            ViewBag.exp = exp;
            ViewBag.match = match;
            if ((wishes != null) && (userId != null))
            {
               // var prods = products.Where(x => wishes.Any(y => ((y.ProductId == x.Id) && (y.UserId == int.Parse(userId)))));
                products.ToList().ForEach(x => 
                { 
                    if (wishes.Any(y => ((y.ProductId == x.Id) && (y.UserId == int.Parse(userId)))))
                    {
                        x.Sku = "wish";

                    }

                });
            }

            return Json(products);  
        
        
        }


       



        
    }
}
