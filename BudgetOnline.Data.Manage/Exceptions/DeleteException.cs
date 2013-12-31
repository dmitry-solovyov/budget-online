using System;

namespace BudgetOnline.Data.Manage.Exceptions
{
	public class DeleteException : DataException
	{
		public DeleteException(Exception innerException)
			: base(innerException)
		{
		}
	}
}
