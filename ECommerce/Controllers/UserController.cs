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

namespace ECommerce.Controllers
{
    public class UserController : BaseController

    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager,
                            SignInManager<User> signInManager) : base(uow, mapper)
        {
            _userManager = userManager; 
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            //_uow.GetContext().Roles.Add(new ApplicationDbContext.Models.UserRole()
            //{
            //    Name = "Admin",
            //    NormalizedName = "ADMIN"
            //});
            //_uow.GetContext().SaveChanges();
            UserRegisterVM userVM = new UserRegisterVM();
            return View(userVM);
        }



        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM userVM)
        {
            if (ModelState.IsValid)
            {
                var user = new User {CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, FirstName = userVM.User.FirstName, LastName = userVM.User.LastName, PhoneNumber = userVM.User.PhoneNumber, Email = userVM.User.Email, UserName = userVM.User.Email };
                var result = await _userManager.CreateAsync(user, userVM.User.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Customer");

                    var addressController = new AddressController(_uow, _mapper);
                    addressController.Create(user.Id, userVM.Addresses);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["success"] = "Registered! Now You Are Logged In!";
                    return RedirectToAction("Login", "User");
                }
                TempData["error"] = "Could Not Register!";
                AddErrors(result);
            }
            RegisterErrorsToView();
            return View(userVM);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("DBError", error.Description);
            }
        }


        [HttpGet]
        public IActionResult Login()
        {
            UserLoginVM userLoginVM = new UserLoginVM();
            return View(userLoginVM);
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM userVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userVM.Email, userVM.Password, false /*remember me*/, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    TempData["success"] = "Logged In Successffuly";
                    return RedirectToAction("Register", "User");
                }
                ModelState.AddModelError("DBError", "Invalid login attempt.");
            }
            LoginErrorsToView();
            return View(userVM);
        }
        private void RegisterErrorsToView()
        {
            
            if (ModelState == null)
                return;
            if (ModelState.ContainsKey("DBError") && ModelState["DBError"].Errors.Count > 0)
            {
                var errMsg = ModelState["DBError"].Errors[0].ErrorMessage;
                errMsg = errMsg.Replace("Username", "Email");
                TempData["error"] = errMsg;
            }
            if (ModelState.ContainsKey("User.FirstName") && ModelState["User.FirstName"].Errors.Count > 0)
            {
                ViewBag.FirstNameErrMsg = ModelState["User.FirstName"].Errors[0].ErrorMessage;
            }

            if(ModelState.ContainsKey("User.LastName") && ModelState["User.LastName"].Errors.Count > 0)

            {
                ViewBag.LastNameErrMsg = ModelState["User.LastName"].Errors[0].ErrorMessage;
            }
            if(ModelState.ContainsKey("User.PhoneNumber") && ModelState["User.PhoneNumber"].Errors.Count > 0)

            {
                ViewBag.PhoneNumberErrMsg = ModelState["User.PhoneNumber"].Errors[0].ErrorMessage;
            }
            if(ModelState.ContainsKey("Addresses") && ModelState["Addresses"].Errors.Count > 0)
            {
                ViewBag.AddressErrMsg = ModelState["Addresses"].Errors[0].ErrorMessage;
            }
            if(ModelState.ContainsKey("User.Email") && ModelState["User.Email"].Errors.Count > 0)
            {
                ViewBag.EmailErrMsg = ModelState["User.Email"].Errors[0].ErrorMessage;
            }
            if(ModelState.ContainsKey("User.Password") && ModelState["User.Password"].Errors.Count > 0)

            {
                ViewBag.PasswordErrMsg = ModelState["User.Password"].Errors[0].ErrorMessage;
            }
            if(ModelState.ContainsKey("User.ConfirmPassword") && ModelState["User.ConfirmPassword"].Errors.Count > 0)
            {
                ViewBag.ConfirmPasswordErrMsg = ModelState["User.ConfirmPassword"].Errors[0].ErrorMessage;
            }
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
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

















        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
