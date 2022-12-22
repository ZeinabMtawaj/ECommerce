

using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationDbContext.Models;
using Ecommerce.Models;

namespace ECommerce.Models.ViewModels
{
    public class SpecificationVM
    {
        public SpecificationVM()
        {
            SpecificationIds = new List<int>();
            SpecificationNames = new List<string>();
            SpecificationValues = new List<string>();
        }
        public List<int> SpecificationIds { get; set; }
        public List<string> SpecificationNames{ get; set; }
        public List<string> SpecificationValues { get; set; }
    }
}
