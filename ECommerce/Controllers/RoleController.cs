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
    public class RoleController : BaseController

    {
        public RoleController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager) : base(uow, mapper, userManager, signInManager, roleManager) 
        { 


        }


        public IEnumerable<string> GetAllRoles()
        {
            return _roleManager.Roles.Select(r => r.Name).ToList();
        }
        
        public IEnumerable<string> GetRoleByUserId(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            return _userManager.GetRolesAsync(user).Result;
        }



        public int GetUsersForRole(string role)
        {
            return _userManager.GetUsersInRoleAsync(role).Result.Count;
        }



        [Authorize]
        public async Task ToggleRoleAsync(string role, string userId)
        {
            var roles = this.GetRoleByUserId(userId).ToList();
            var user = _userManager.FindByIdAsync(userId).Result;
            if (roles.Contains(role))
                await _userManager.RemoveFromRoleAsync(user, role);
            else
                await _userManager.AddToRoleAsync(user, role);
            
            _uow.SaveChanges();

        }
    }
}
