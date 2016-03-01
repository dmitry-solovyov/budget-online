using System;
using System.Linq.Expressions;

namespace BudgetOnline.Data.Manage.Contracts
{
	public interface IRepository<T> 
		where T: class 
	{
		T Insert(T record);
		void Update(T record);
		void Delete(Expression<Func<T, bool>> selector);
	}
}
