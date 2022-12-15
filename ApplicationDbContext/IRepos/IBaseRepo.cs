﻿using System.Collections.Generic;

namespace ApplicationDbContext.IRepos
{
    public interface IBaseRepo<T> where T : class
    {
        T Find(int id);

        // T Get(string id);

        void Add(T item);

        void Update(T item);

        void Delete(int id);

        IEnumerable<T> GetAll();
    }
}
