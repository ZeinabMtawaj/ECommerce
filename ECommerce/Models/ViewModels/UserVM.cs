

using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationDbContext.Models;
using Ecommerce.Models;

namespace ECommerce.Models.ViewModels
{
    public class UserVM
    {
        public UserVM()
        {
            Addresses = new List<string>();
            User = new UserViewModel();
        }
        public UserViewModel?  User{ get; set; }
        public IEnumerable<string?>Addresses { get; set; }

    }
}
