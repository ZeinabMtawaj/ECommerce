

using Microsoft.AspNetCore.Mvc.Rendering;

using Ecommerce.Models;

namespace ECommerce.Models.ViewModels
{
    public class ProductVM
    {
        public ProductViewModel? Product { get; set; }
        public IEnumerable<string>? Specifications { get; set; }

        public IEnumerable<string>? categories { get; set; }

        public IEnumerable<string>? SpecificationValues { get; set; }
    }
}
