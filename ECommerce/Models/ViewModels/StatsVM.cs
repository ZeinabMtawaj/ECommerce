

using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationDbContext.Models;
using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.ViewModels
{
    public class StatsVM
    {
        
        public Dictionary<string, int> Role_Users { get; set; }
        public Dictionary<string, int> Category_Products { get; set; }
        public Dictionary<string, int> Order_States { get; set; }
    }
}
