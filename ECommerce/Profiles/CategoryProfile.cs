using AutoMapper;
using ApplicationDbContext.Models;
using Ecommerce.Models;

namespace ECommerce.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
