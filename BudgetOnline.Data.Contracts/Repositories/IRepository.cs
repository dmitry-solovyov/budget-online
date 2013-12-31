using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BudgetOnline.Data.Contracts.Repositories
{
	public interface IRepository<T> 
		where T: class 
	{
		T GetSingle(Expression<Func<T, bool>> selector);
		IEnumerable<T> GetList(Expression<Func<T, bool>> selector);

		void Insert(T record);
		void Update(T record);
		void Delete(Expression<Func<T, bool>> selector);
	}
}
