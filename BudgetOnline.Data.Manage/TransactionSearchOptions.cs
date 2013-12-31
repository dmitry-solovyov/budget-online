using System;
using System.Collections.Generic;

namespace BudgetOnline.Data.Manage
{
	public class TransactionSearchOptions
	{
		public DateTime? Date1 { get; set; }
		public DateTime? Date2 { get; set; }

		public string SearchText { get; set; }
		public string Tag { get; set; }
		public string Description { get; set; }

		public IEnumerable<int> TransactionTypes { get; set; }
		public IEnumerable<int> Currencues { get; set; }
		public IEnumerable<int> Accounts { get; set; }
		public IEnumerable<int> Categories { get; set; }

		public IEnumerable<string> ExcludeTags { get; set; }

		public int? PageSize { get; set; }
		public int? PageNumber { get; set; }

		public int SumSign { get; set; }
	}
}
