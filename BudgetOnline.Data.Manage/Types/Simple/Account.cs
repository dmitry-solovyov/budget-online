using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class Account
	{
		public int Id { get; set; }
		public int SectionId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public bool ShowForIncome { get; set; }
		public bool ShowForOutcome { get; set; }
		public bool ShowForTransfer { get; set; }

		public bool IsDefault { get; set; }
		public bool IsDisabled { get; set; }
        public bool IsExternal { get; set; }
		
		public int? CreatedBy { get; set; }
		public DateTime CreatedWhen { get; set; }
		public int? UpdatedBy { get; set; }
		public DateTime? UpdatedWhen { get; set; }
	}
}
