using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Ecommerce.Models;
using AutoMapper;
using ApplicationDbContext.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ECommerce.Models.ViewModels;

namespace Ecommerce.Controllers

{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {

        
        public AdminController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager) : base(uow, mapper, userManager, signInManager, roleManager)
        {
        }

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

            Dictionary <string , int> role_users = new Dictionary<string , int>();
            var roleController = new RoleController(_uow, _mapper, _userManager, _signInManager, _roleManager);
            var roles = roleController.GetAllRoles();
            foreach (var role in roles)
            {
                role_users.Add(role, roleController.GetUsersForRole(role));
            }

            var categoryController = new CategoryController(_uow, _mapper, _userManager, _signInManager, _roleManager);
            var categories = categoryController.getAllCategories();
            Dictionary<string, int> category_products = new Dictionary<string, int>();
            var productController = new ProductController(_uow, _mapper, _userManager, _signInManager, _roleManager);
            foreach(var category in categories)
            {
                category_products.Add(category.Name, productController.GetProductsForCategory(category.Id));
            }


            var orderController = new OrderController(_uow, _mapper, _userManager, _signInManager, _roleManager);
            var orders = orderController.GetAllOrders();
            Dictionary<string, int> order_states = new Dictionary<string, int>();
            foreach (var order in orders)
            {
                if(order_states.ContainsKey(order.State))
                    order_states[order.State]++;
                else
                    order_states.Add(order.State, 1);
            }
            order_states.Add("Delivered", 15);
            order_states.Add("Delivering", 20);
            order_states.Add("Pending", 50);
            var model = new StatsVM()
            {
                Category_Products = category_products,
                Order_States = order_states,
                Role_Users = role_users
            };
            ViewBag.Page = "Admin";
            return View(model);
        }

    }
}
