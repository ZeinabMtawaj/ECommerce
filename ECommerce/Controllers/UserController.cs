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
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Controllers
{
    public class UserController : BaseController

    {
        public UserController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {
            
        }


        

        [HttpGet]
        [Route("Account/Login")]
        public IActionResult Login()
        {
            UserLoginVM userLoginVM = new UserLoginVM();
            return View(userLoginVM);
        }


        [HttpPost]
        [Route("Account/Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVM userVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userVM.Email, userVM.Password, false /*remember me*/, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(userVM.Email);
                    string userId = user.Id.ToString();
                    HttpContext.Session.SetString("UserId", userId);
                    TempData["success"] = "Logged In Successfuly";
                    return RedirectToAction("Dashboard", "Admin");
                }
                ModelState.AddModelError("DBError", "Invalid login attempt.");
            }
            LoginErrorsToView();
            return View(userVM);
        }
       
        private void LoginErrorsToView()
        {
            if (ModelState == null)
                return;
            if (ModelState.ContainsKey("DBError") && ModelState["DBError"].Errors.Count > 0)
            {
                TempData["error"] = ModelState["DBError"].Errors[0].ErrorMessage;
            }
           
            if (ModelState.ContainsKey("Email") && ModelState["Email"].Errors.Count > 0)
            {
                ViewBag.EmailErrMsg = ModelState["Email"].Errors[0].ErrorMessage;
            }
            if (ModelState.ContainsKey("Password") && ModelState["Password"].Errors.Count > 0)

            {
                ViewBag.PasswordErrMsg = ModelState["Password"].Errors[0].ErrorMessage;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Remove("UserId");
            TempData["success"] = "Logged Out Successfully!";
            return Redirect("https://localhost:7068");
        }



    }
}
