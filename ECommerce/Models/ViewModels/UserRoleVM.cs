

using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationDbContext.Models;
using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.ViewModels
{
    public class UserRoleVM
    {
        public UserRoleVM()
        {
            Roles = new List<string>(); 
            Users = new List<User>();
        }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
