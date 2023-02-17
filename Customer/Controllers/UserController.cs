using ApplicationDbContext.UOW;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Customer.Models.ViewModels;
using ApplicationDbContext.Models;
using Microsoft.AspNetCore.Identity;
using Customer.Models;
using Customer.Controllers;
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
            //_uow.GetContext().Roles.Add(new ApplicationDbContext.Models.UserRole()
            //{
            //    Name = "Admin",
            //    NormalizedName = "ADMIN"
            //});
            //_uow.GetContext().SaveChanges();
            //_uow.GetContext().Roles.Add(new ApplicationDbContext.Models.UserRole()
            //{
            //    Name = "Customer",
            //    NormalizedName = "CUSTOMER"
            //});
            //_uow.GetContext().SaveChanges();

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
            ErrorsToView("User.");
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
            ErrorsToView("");
            return View(userVM);
        }
        private void ErrorsToView(string entity)
        {
            
            if (ModelState == null)
                return;
            if (ModelState.ContainsKey("DBError") && ModelState["DBError"].Errors.Count > 0)
            {
                var errMsg = ModelState["DBError"].Errors[0].ErrorMessage;
                errMsg = errMsg.Replace("Username", "Email");
                TempData["error"] = errMsg;
            }
            if (ModelState.ContainsKey(entity + "FirstName") && ModelState[entity + "FirstName"].Errors.Count > 0)
            {
                ViewBag.FirstNameErrMsg = ModelState[entity + "FirstName"].Errors[0].ErrorMessage;
            }

            if(ModelState.ContainsKey(entity + "LastName") && ModelState[entity + "LastName"].Errors.Count > 0)

            {
                ViewBag.LastNameErrMsg = ModelState[entity + "LastName"].Errors[0].ErrorMessage;
            }
            if(ModelState.ContainsKey(entity + "PhoneNumber") && ModelState[entity + "PhoneNumber"].Errors.Count > 0)

            {
                ViewBag.PhoneNumberErrMsg = ModelState[entity + "PhoneNumber"].Errors[0].ErrorMessage;
            }
            if(ModelState.ContainsKey("Addresses") && ModelState["Addresses"].Errors.Count > 0)
            {
                ViewBag.AddressErrMsg = ModelState["Addresses"].Errors[0].ErrorMessage;
            }
            if(ModelState.ContainsKey(entity + "Email") && ModelState[entity + "Email"].Errors.Count > 0)
            {
                ViewBag.EmailErrMsg = ModelState[entity + "Email"].Errors[0].ErrorMessage;
            }
            if(ModelState.ContainsKey(entity + "Password") && ModelState[entity + "Password"].Errors.Count > 0)

            {
                ViewBag.PasswordErrMsg = ModelState[entity + "Password"].Errors[0].ErrorMessage;
            }
            if(ModelState.ContainsKey(entity + "ConfirmPassword") && ModelState[entity + "ConfirmPassword"].Errors.Count > 0)
            {
                ViewBag.ConfirmPasswordErrMsg = ModelState[entity + "ConfirmPassword"].Errors[0].ErrorMessage;
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


        [HttpGet]
        public IActionResult Profile()
        {
            UserProfileVM profileVM = new UserProfileVM();
            var userId = HttpContext.Session.GetString("UserId");

            var user = _userManager.FindByIdAsync(userId).Result;

            profileVM.Profile.FirstName = user.FirstName;
            profileVM.Profile.LastName = user.LastName;
            profileVM.Profile.Email = user.Email;
            profileVM.Profile.PhoneNumber = user.PhoneNumber;
            var addressController = new AddressController(_uow, _mapper);
            var addresses = addressController.getAddressByUserId(user.Id).ToList();

            foreach(var add in addresses)
            {
                profileVM.Addresses.Add(add.Location);
            }


            string currUrl = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(currUrl))
            {
                HttpContext.Session.SetString("PrevUrl", currUrl);
            }
            return View(profileVM);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(UserProfileVM profileVM)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetString("UserId");
                var user = _userManager.FindByIdAsync(userId).Result;
                
                if(user.FirstName != profileVM.Profile.FirstName)
                {
                    user.FirstName = profileVM.Profile.FirstName;
                    user.UpdatedDate = DateTime.Now;
                }
                if (user.LastName != profileVM.Profile.LastName)
                {
                    user.LastName = profileVM.Profile.LastName;
                    user.UpdatedDate = DateTime.Now;
                }
                if (user.Email != profileVM.Profile.Email)
                {
                    user.Email = profileVM.Profile.Email;
                    user.UpdatedDate = DateTime.Now;
                }
                if (user.PhoneNumber != profileVM.Profile.PhoneNumber)
                {
                    user.PhoneNumber = profileVM.Profile.PhoneNumber;
                    user.UpdatedDate = DateTime.Now;
                }
                bool passError = false;
                if (profileVM.Profile.Password != null && profileVM.Profile.OldPassword != null)
                { 
                    var passRes = await _userManager.ChangePasswordAsync(user, profileVM.Profile.OldPassword, profileVM.Profile.Password);
                    if (passRes.Succeeded)
                    {
                        user.UpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        passError = true;
                        AddErrors(passRes);
                        TempData["error"] = "Something Went Wrong!";
                    }
                }
                if (!passError)
                {
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var addressController = new AddressController(_uow, _mapper);
                        addressController.Update(user.Id, profileVM.Addresses);
                        TempData["success"] = "Profile Saved!";

                        string prevUrl = HttpContext.Session.GetString("PrevUrl");
                        if (!string.IsNullOrEmpty(prevUrl))
                        {
                            return Redirect(prevUrl);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    TempData["error"] = "Something Went Wrong!";
                    AddErrors(result);
                }
            }
            ErrorsToView("Profile.");
            return View(profileVM);
        }

    }
}
