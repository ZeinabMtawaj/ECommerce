using ApplicationDbContext.UOW;
using AutoMapper;
using Customer.Controllers;
using Microsoft.AspNetCore.Mvc;
using Customer.Models;
using Customer.Models.ViewModels;
using ApplicationDbContext.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Customer.Controllers
{
    public class UserController : BaseController

    {
        public UserController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {
            
        }


        [HttpGet]
        public IActionResult Register()
        {
           /* _uow.GetContext().Roles.Add(new ApplicationDbContext.Models.UserRole()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
            _uow.GetContext().SaveChanges();
            _uow.GetContext().Roles.Add(new ApplicationDbContext.Models.UserRole()
            {
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            });
            _uow.GetContext().SaveChanges();
*/
            UserRegisterVM userVM = new UserRegisterVM();
            return View(userVM);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterVM userVM)
        {
            if (ModelState.IsValid)
            {
                var user = new User {CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, FirstName = userVM.User.FirstName, LastName = userVM.User.LastName, PhoneNumber = userVM.User.PhoneNumber, Email = userVM.User.Email, UserName = userVM.User.Email };
                var result = await _userManager.CreateAsync(user, userVM.User.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");

                    var addressController = new AddressController(_uow, _mapper);
                    addressController.Create(user.Id, userVM.Addresses);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["success"] = "Registered! Now You Are Logged In!";
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    return RedirectToAction("Index", "Home");
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
                    return RedirectToAction("Index", "Home");
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Remove("UserId");
            TempData["success"] = "Logged Out Successfully!";
            return RedirectToAction("Index", "Home");
        }




        [HttpGet]
        public IActionResult Dashboard()
        {
            return Redirect("https://localhost:7156");
        }

    }
}
