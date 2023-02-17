using ApplicationDbContext.IRepos;
using ApplicationDbContext.Models;
using ApplicationDbContext.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationDbContext.UOW
{
    public class UnitOfWork : IUnitOfWork
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

        public IUserRepo UserRepo { get; set; }





        protected readonly ECommerceDBContext _db;

        public UnitOfWork(ECommerceDBContext db)
        {
            _db = db;
            ProductRepo = new ProductRepo(db);
            CategoryRepo = new CategoryRepo(db);
            SpecificationRepo = new SpecificationRepo(db);
            CategorySpecificationValueRepo = new CategorySpecificationValueRepo(db);
            ProductSpecificationValueRepo = new ProductSpecificationValueRepo(db);
            PhotoRepo = new PhotoRepo(db);
            TrendRepo = new TrendRepo(db);
            AddressRepo = new AddressRepo(db);
            WishListRepo = new WishListRepo(db);
            UserRepo = new UserRepo(db);





        }
        public ECommerceDBContext GetContext()
        {
            return _db;
        }

        public void RollBack()
        {
            _db.Dispose();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
