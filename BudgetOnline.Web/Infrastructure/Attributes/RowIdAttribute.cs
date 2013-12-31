using System;

namespace BudgetOnline.Web.Infrastructure.Attributes
{
	public class RowIdAttribute : Attribute
	{
		private readonly int _id;

		public RowIdAttribute(int id)
		{
			_id = id;
		}

		public int RowId
		{
			get { return _id; }
		}
	}
}