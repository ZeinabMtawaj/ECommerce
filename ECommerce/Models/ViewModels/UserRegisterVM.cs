

using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationDbContext.Models;
using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.ViewModels
{
    public class UserRegisterVM
    {
        public UserRegisterVM()
        {
            Addresses = new List<string>();
            User = new UserViewModel();
        }
        public UserViewModel?  User{ get; set; }
        [Required(ErrorMessage = "Required")]
        public IEnumerable<string>Addresses { get; set; }

    }
}
