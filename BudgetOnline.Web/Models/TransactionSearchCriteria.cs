using System;
using System.Collections.Generic;

namespace BudgetOnline.Web.Models
{
	public class TransactionSearchCriteria<TData, TStat, TSearch> : SearchCriteria<TData, TSearch>
	{
		public Func<IEnumerable<TStat>> Statistics { get; set; }
	}
}