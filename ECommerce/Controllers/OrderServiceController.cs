using ApplicationDbContext.Models;
using ApplicationDbContext.UOW;
using AutoMapper;
using Ecommerce.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/OrderService")]
[ApiController]
public class OrderServiceController : BaseController
{
    public OrderServiceController(IWebHostEnvironment environment, IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager) : base(uow, mapper, userManager, signInManager, roleManager)
    {
       
    }



    [HttpGet("{id}")]
    public IActionResult GetState(int id)
    {
        var order = _uow.OrderRepo.Find(x => x.Id == id);
        string msg = "No Such Order";
        if (order != null)
            msg = order.State;
        ViewBag.msg = msg;
        return View();
    }
}
