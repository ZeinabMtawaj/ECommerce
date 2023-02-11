using ApplicationDbContext.UOW;
using AutoMapper;
using Ecommerce.Controllers;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Models;
using ECommerce.Models.ViewModels;
using ApplicationDbContext.Models;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Controllers
{
    public class BaseController : Controller
    {
        protected IUnitOfWork _uow;

        protected readonly IMapper _mapper;

        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;
        public BaseController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
    }
}
