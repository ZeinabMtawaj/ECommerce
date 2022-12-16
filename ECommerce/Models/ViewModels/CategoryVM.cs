

using Microsoft.AspNetCore.Mvc.Rendering;

using Ecommerce.Models;

namespace ECommerce.Models.ViewModels
{
    public class CategoryVM
    {
        public CategoryViewModel? Category { get; set; }
        public IEnumerable<SelectListItem>? Specifications { get; set; }
        //public IEnumerable<string>? SpecificationValues { get; set; }
    }
}
