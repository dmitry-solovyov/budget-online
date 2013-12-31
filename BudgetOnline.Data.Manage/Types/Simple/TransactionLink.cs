using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class TransactionLink
	{
		public int Id { get; set; }
		public int ParentId { get; set; }
		public int ChildId { get; set; }

		public int CreatedBy { get; set; }
		public DateTime CreatedWhen { get; set; }
	}
}
