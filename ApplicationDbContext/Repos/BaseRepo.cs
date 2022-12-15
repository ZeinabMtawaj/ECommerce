using ApplicationDbContext.IRepos;
using ApplicationDbContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationDbContext.Repos
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        protected ECommerceDBContext _db;

        private DbSet<T> _dbSet;
        public BaseRepo(ECommerceDBContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public T Find(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(T item)
        {
            _dbSet.Update(item);
        }
    }
}
