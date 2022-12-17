using AutoMapper;
using ApplicationDbContext.Models;
using Ecommerce.Models;

namespace ECommerce.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>();
        }
    }
}
