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
using System.Text.Json;

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
            getUserFromSession();
            List<ProductOrder> cart = new List<ProductOrder>();
            List<Product> prods = new List<Product>();

            if (ViewBag.UserName != null)
            {
                cart = ViewBag.Cart;
                if (cart != null)
                {
                    foreach (var item in cart)
                    {
                        var prod = _uow.ProductRepo.Find(x => x.Id == item.ProductId);
                        prods.Add(prod);
                    }
                }





            }
            OrderVM orderVM = new OrderVM()
            {
                Products = prods,
                ProductsOrder = cart
            };

            return View(orderVM);

        }

        public IActionResult DeleteAll()
        {

            getUserFromSession();
            var cart = new List<ProductOrder>();
            string serializeCart = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString("Cart", serializeCart);
            List<Product> prods = new List<Product>();
            OrderVM orderVM = new OrderVM()
            {
                Products = prods,
                ProductsOrder = cart
            };
            return View("~/Views/Order/Detail.cshtml",orderVM);
           
        }
        public IActionResult Create()
        {
            getUserFromSession();


            UserController userCon = new UserController(_uow, _mapper, _userManager, _signInManager);
            string? userId = ViewBag.UserId;
            IEnumerable<WishList> wishes = null;
            if (userId != null) {
              
                wishes = userCon.getWishList(userId);
                var cartElements = new List<ProductOrder>();
                cartElements = ViewBag.Cart;
                decimal? totalPrice = 0;
                foreach (var elem in cartElements)
                {

                    totalPrice += (elem.Price) * (elem.Quantity);

                }
                int orderId = 0;
                if (totalPrice != 0)
                {
                    var order1 = new Order()
                    {
                        State = "Pending",
                        CustomerId = int.Parse(userId),
                        Total = totalPrice

                    };
                    _uow.OrderRepo.Add(order1);
                    _uow.SaveChanges();
                    orderId = order1.Id;

                    foreach (var elem in cartElements)
                    {
                        var productOrder = new ProductOrder()
                        {
                            ProductId = elem.ProductId,
                            Quantity = elem.Quantity,
                            Price = elem.Price,
                            OrderId = orderId

                        };
                        _uow.ProductOrderRepo.Add(productOrder);
                        _uow.SaveChanges();
                    }

                    TempData["success"] = "Order Created Successfully!";


                }
                else
                {
                    TempData["error"] = "Cart Is Empty!";
                }
            }

            var cart = new List<ProductOrder>();
            string serializeCart = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString("Cart", serializeCart);


          
            IEnumerable<Product> products = null;
            int take = 20;          
            products = _uow.ProductRepo.FindAll(x => x.Id >= 1, take, 0);
            ViewBag.subtitle = "All Products";
            ViewBag.exp = "all";
            ViewBag.match = "";
            if ((wishes != null) && (userId != null))
            {
                var prods = products.Where(x => wishes.Any(y => ((y.ProductId == x.Id) && (y.UserId == int.Parse(userId)))));
                prods.ToList().ForEach(x => x.Sku = "wish");
            }

            return View("~/Views/Product/Index.cshtml", products);



        }


    }
}
