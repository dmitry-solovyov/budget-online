using System;
using System.Collections.Generic;

namespace BudgetOnline.Data.Contracts
{
    public interface IRepository<T, in TId> : IDisposable
        where T : class
    {
        IEnumerable<T> GetAll();
        //IEnumerable<T> Find(Expression<Func<T, bool>> query);

        T GetById(TId id);
        
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
