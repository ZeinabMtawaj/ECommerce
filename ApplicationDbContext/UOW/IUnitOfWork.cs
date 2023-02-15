using ApplicationDbContext.IRepos;
using System;
using ApplicationDbContext.Models;
using System.Collections.Generic;
using System.Text;

namespace ApplicationDbContext.UOW
{
    public interface IUnitOfWork
    {
        public IProductRepo ProductRepo { get; set; }

        public ICetegoryRepo CategoryRepo { get; set; }

        public ISpecificationRepo SpecificationRepo { get; set; }

        public ICategorySpecificationValueRepo CategorySpecificationValueRepo { get; set; }

        public IProductSpecificationValueRepo ProductSpecificationValueRepo { get; set; }

        public IPhotoRepo PhotoRepo { get; set; }

        public ITrendRepo TrendRepo { get; set; }

        public IAddressRepo AddressRepo { get; set; }

        public IWishListRepo WishListRepo { get; set; }


        public ECommerceDBContext GetContext();
        public void SaveChanges ();

        public void RollBack();
    }
}
