

using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationDbContext.Models;
using Customer.Models;

namespace Customer.Models.ViewModels
{
    public class CategoryVM
    {
        public CategoryVM()
        {
            SpecificationValues = new List<string>();
            SpecificationIds = new List<string>();
        }
        public CategoryViewModel? Category { get; set; }
        public IEnumerable<string?>SpecificationValues { get; set; }
        public IEnumerable<string?> SpecificationIds { get; set; }

    }
}
