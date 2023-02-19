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
using System.Linq.Expressions;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController

    {
        public UserController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager) : base(uow, mapper, userManager, signInManager, roleManager)
        {
        }


        

        [HttpGet]
        [Route("Account/Login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginVM userLoginVM = new UserLoginVM();
            return View(userLoginVM);
        }


        [HttpPost]
        [Route("Account/Login")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? Id)
        {
            User? obj = null;
            if (Id != null)
            {
                var lambdas = new List<Expression<Func<User, object>>>();
                lambdas.Add(x => x.Ratings);
                lambdas.Add(x => x.WishLists);
                lambdas.Add(x => x.Orders);
                lambdas.Add(x => x.Addresses);
                obj = _uow.UserRepo.Find(x => (x.Id == Id), lambdas);
            }
            if (obj == null)
            {
                TempData["error"] = "Something Went Wront..";
            }
            else
            {
                if ((obj.Ratings.Count() != 0) || (obj.WishLists.Count() != 0) || (obj.Orders.Count() != 0) || (obj.Addresses.Count() != 0))
                {
                    TempData["error"] = "Something Went Wront..";
                }
                else
                {
                    var user = _userManager.FindByIdAsync(Id.ToString()).Result;
                    var result = _userManager.DeleteAsync(user).Result;
                    if (result.Succeeded)
                    {
                        TempData["success"] = "Deleted Successfully";
                        _uow.SaveChanges();
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        TempData["error"] = "Something Went Wrong..";
                    }
                        
                }

            }
            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
            getUserFromSession();
            ViewBag.deleteController = "User";
            ViewBag.deleteAction = "Delete";
            var roleController = new RoleController(_uow, _mapper, _userManager, _signInManager, _roleManager);
            var model = new UserRoleVM()
            {
                Users = _userManager.Users.ToList(),
                Roles = roleController.GetAllRoles()
            };
            ViewBag.Token = Request.Cookies[".AspNetCore.Identity.Application"];

            ViewBag.Page = "User";  
            return View(model);
        }

    }
}
