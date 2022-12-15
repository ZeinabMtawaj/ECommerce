using System.Collections.Generic;

namespace ApplicationDbContext.IRepos
{
    public interface IBaseRepo<T> where T : class
    {
        T Find(int id);

        T Find(Expression<Func<T,bool>> match, string[] includes =null);

        void Add(T item);

        void Update(T item);

        void Delete(int id);

        IEnumerable<T> GetAll();
    }
}
