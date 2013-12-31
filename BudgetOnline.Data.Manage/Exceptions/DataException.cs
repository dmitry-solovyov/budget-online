using System;

namespace BudgetOnline.Data.Manage.Exceptions
{
	public class DataException : Exception
	{
		public DataException()
		{ }

		public DataException(string message)
			: base(message)
		{ }

		public DataException(Exception exception)
			: base("Data exception occurred", exception)
		{ }

		public DataException(string message, Exception exception)
			: base(message, exception)
		{ }
	}
}
