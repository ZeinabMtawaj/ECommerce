using ApplicationDbContext.IRepos;
using ApplicationDbContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public void Delete(T item)
        {
            _dbSet.Remove(item);
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
            //_db.Attach(item);   
            //_db.Entry(item).State = EntityState.Modified;
            _dbSet.Update(item);
        }

        public T Find(Expression<Func<T, bool>> match, List<Expression<Func<T, object>>> lambdas = null)
        {
            IQueryable<T> query = _dbSet;
            //if (includes != null)
            //{
            //    foreach (var include in includes)
            //    {
            //        query = query.Include(include);
            //    }
            //}
            if (lambdas != null)
            {
                foreach (var lambda in lambdas)
                    query = query.Include(lambda);
            }

            return query.FirstOrDefault(match);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int? take = null, int? skip = null, List<Expression<Func<T, object>>>? lambdas = null, Expression<Func<T, Object>> ?orderBy = null, String? orderByDirection = null)
        {
            IQueryable<T> query = _dbSet.Where(match);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);
            if (lambdas != null)
            {
                foreach (var include in lambdas)
                {
                    query = query.Include(include);
                }
            }
            if (orderBy != null)
            {
                if (orderByDirection == "DESC")
                {
                    query = query.OrderByDescending(orderBy);
                }
                else
                {
                    query = query.OrderBy(orderBy);
                }
            }
            return query.ToList();

        }

    }
}