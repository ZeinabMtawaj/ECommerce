using AutoMapper;
using ApplicationDbContext.Models;
using Ecommerce.Models;
using ECommerce.Models.ViewModels;
namespace ECommerce.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<CategoryVM, Category>()
            .ForMember(dest =>
                dest.Name,
                opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest =>
                dest.Image,
                opt => opt.MapFrom(src => src.Category.Image))
            .ForMember(dest =>
                dest.Description,
                opt => opt.MapFrom(src => src.Category.Description));
        }
    }
}
