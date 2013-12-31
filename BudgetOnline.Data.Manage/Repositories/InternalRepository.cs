using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using BudgetOnline.Data.Manage.Exceptions;
using BudgetOnline.Data.Manage.Helpers;

namespace BudgetOnline.Data.Manage.Repositories
{
    public abstract class InternalRepository<TInternal, TExported> //: IRepository<TExported>
        where TInternal : class
        where TExported : class
    {
        //public InternalRepository() : this(null, null) { }
        //public InternalRepository(Table<T> source) : this(source, null) { }
        //public InternalRepository(Table<T> source, DbTransaction transaction)
        //{
        //    _source = source;
        //    _transaction = transaction;
        //}

        protected readonly TypeMappingHelper<TInternal, TExported> MappingHelper =
            new TypeMappingHelper<TInternal, TExported>();

        public abstract Table<TInternal> Source { get; }

        private readonly DbTransaction _transaction;

        protected TInternal GetSingleInternal(Expression<Func<TInternal, bool>> selector)
        {
            var source = Source;
            return source.SingleOrDefault(selector);
        }
        public virtual TExported GetSingle(Expression<Func<TInternal, bool>> selector)
        {
            return MappingHelper.OutMapper(GetSingleInternal(selector));
        }

        protected IQueryable<TInternal> GetListInternal()
        {
            return Source;
        }

        public virtual IEnumerable<TExported> GetList()
        {
            return GetList(null);
        }

        public virtual IEnumerable<TExported> GetList(Expression<Func<TInternal, bool>> selector)
        {
            if (selector == null)
                return GetMappedItems(GetListInternal()).ToList();

            return GetMappedItems(GetListInternal().Where(selector)).ToList();
        }

        //public virtual IEnumerable<TExported> GetList(Expression<Func<TInternal, bool>> selector, Expression<Func<TInternal, TKey>> orderby, bool desc = false)
        //{
        //    var items = GetListInternal();

        //    if(selector != null)
        //        items = items.Where(selector);

        //    if (orderby != null)
        //        items = desc ? items.OrderByDescending(@orderby) : items.OrderBy(@orderby);

        //    return GetMappedItems(items).ToList();
        //}

        protected virtual IEnumerable<TExported> GetMappedItems(IEnumerable<TInternal> internalList)
        {
            if (internalList == null)
                return Enumerable.Empty<TExported>();

            return internalList.Select(row => MappingHelper.OutMapper(row));
        }

        public virtual TExported Insert(TExported record)
        {
            var source = Source;

            if (_transaction != null)
                source.Context.Transaction = _transaction;

            try
            {
                var internalObj = MappingHelper.InMapper(record);
                source.InsertOnSubmit(internalObj);
                source.Context.SubmitChanges();

                return MappingHelper.OutMapper(internalObj);
            }
            catch (Exception ex)
            {
                throw new InsertException(ex);
            }
        }

        protected void UpdateInternal(Expression<Func<TInternal, bool>> selector, Action<TInternal> updater)
        {
            var source = Source;

            if (_transaction != null)
                source.Context.Transaction = _transaction;

            try
            {
                var row = source.Single(selector);

                updater(row);

                source.Context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new UpdateException(ex);
            }
        }

        public virtual void Delete(Expression<Func<TInternal, bool>> selector)
        {
            var source = Source;

            if (_transaction != null)
                source.Context.Transaction = _transaction;

            var items = source.Where(selector);
            //if (1 == items.Count())
            //{
            try
            {
                foreach (var item in items)
                {
                    source.DeleteOnSubmit(item);
                    source.Context.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                new DeleteException(ex);
            }
            //}
            //else
            //	throw new DeleteAffectSeveralRowsException();
        }
    }
}
