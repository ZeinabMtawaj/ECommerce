using AutoMapper;
using ApplicationDbContext.Models;
using Ecommerce.Models;

namespace ECommerce.Profiles
{
    public class SpecificationProfile:Profile
    {
        public SpecificationProfile()
        {
            CreateMap<Specification, SpecificationViewModel>();
        }
    }
}
