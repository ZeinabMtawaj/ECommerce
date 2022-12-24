using ApplicationDbContext.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApplicationDbContext.IRepos
{
    public interface IBaseRepo<T> where T : class
    {
        void Add(T item);

        void Update(T item);

        void Delete(T item);

        void Delete(int id);

        IEnumerable<T> GetAll();

        T Find(int id);

        T Find(Expression<Func<T, bool>> match, List<Expression<Func<T, object>>> lambdas=null);

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int? take, int? skip, List<Expression<Func<T, object>>> lambdas = null, Expression<Func<T, Object>> orderBy = null, String orderByDirection = null);


    }
}
