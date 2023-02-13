﻿using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ApplicationDbContext.Models;

namespace Customer.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {
           
        }

        public IActionResult Index()
        {
            getUserFromSession();
            TrendController trendCon = new TrendController(_uow, _mapper);
            var trends = trendCon.GetAll();
            return View(trends);
        }
    }
}
