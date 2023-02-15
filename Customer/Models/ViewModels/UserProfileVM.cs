

using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationDbContext.Models;
using Customer.Models;
using System.ComponentModel.DataAnnotations;

namespace Customer.Models.ViewModels
{
    public class UserProfileVM
    {
        public UserProfileVM()
        {
            Addresses = new List<string>();
            Profile = new ProfileViewModel();
        }
        public ProfileViewModel?  Profile{ get; set; }
        [Required(ErrorMessage = "Required")]
        public List<string>Addresses { get; set; }

    }
}
