using ApplicationDbContext.UOW;
using AutoMapper;
using Ecommerce.Controllers;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models;
using ECommerce.Models.ViewModels;
using ApplicationDbContext.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : BaseController

    {
        public OrderController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager) : base(uow, mapper, userManager, signInManager, roleManager) 
        { 


        }


        public IEnumerable<Order> GetAllOrders()
        {
            return new List<Order>();
        }
    }
}
