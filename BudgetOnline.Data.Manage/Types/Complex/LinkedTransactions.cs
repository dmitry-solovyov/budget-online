using BudgetOnline.Data.Manage.Types.Simple;

namespace BudgetOnline.Data.Manage.Types.Complex
{
	public class LinkedTransactions
	{
		public bool IsLinked { get { return Second != null; } }
		public Transaction First { get; set; }
		public Transaction Second { get; set; }
	}
}
