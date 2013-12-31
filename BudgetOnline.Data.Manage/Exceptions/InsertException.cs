using System;

namespace BudgetOnline.Data.Manage.Exceptions
{
	public class InsertException : DataException
	{
		public InsertException(Exception innerException)
			: base(innerException)
		{
		}
	}
}
