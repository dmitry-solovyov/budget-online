using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class TransactionTag
	{
		public int Id { get; set; }
		public int SectionId { get; set; }
		public int TransactionId { get; set; }
		public int? TagId { get; set; }

		public string Tag { get; set; }

		public bool IsDisabled { get; set; }
		
		public int? CreatedBy { get; set; }
		public DateTime CreatedWhen { get; set; }
		public int? UpdatedBy { get; set; }
		public DateTime? UpdatedWhen { get; set; }
	}
}
