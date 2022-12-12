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

        protected readonly ECommerceDBContext _db;

        public UnitOfWork(ECommerceDBContext db)
        {
            _db = db;
            ProductRepo = new ProductRepo(db);
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
