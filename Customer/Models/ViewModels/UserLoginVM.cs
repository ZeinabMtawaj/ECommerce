

using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationDbContext.Models;
using Customer.Models;
using System.ComponentModel.DataAnnotations;

namespace Customer.Models.ViewModels
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
