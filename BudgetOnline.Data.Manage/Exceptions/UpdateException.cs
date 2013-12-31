using System;

namespace BudgetOnline.Data.Manage.Exceptions
{
	public class UpdateException : DataException
	{
		public UpdateException(Exception innerException)
			: base(innerException)
		{
		}
	}
}
