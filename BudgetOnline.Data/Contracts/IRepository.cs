using System;
using System.Collections.Generic;
using BudgetOnline.Data.Keys;
using BudgetOnline.Data.QueryManagement;

namespace BudgetOnline.Data.Contracts
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        //IEnumerable<T> Find(OperationsGroup group);
        T GetById(IKeyField key);
        IEnumerable<T> GetAll();
        void Add(T item);
        void Add(IEnumerable<T> items);
        void Update(T item);
        void Delete(T item);
    }
}
