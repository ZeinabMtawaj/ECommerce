

using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationDbContext.Models;
using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.ViewModels
{
    public class UserLoginVM
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Required")]
        public string?  Email{ get; set; }
        [Required(ErrorMessage = "Required")]
        public string? Password { get; set; }

    }
}
