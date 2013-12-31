using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class Tag
	{
		public int Id { get; set; }
		public int SectionId { get; set; }
		public string Name { get; set; }
		public int Hits { get; set; }

		public bool IsDisabled { get; set; }
		
		public int? CreatedBy { get; set; }
		public DateTime CreatedWhen { get; set; }
		public int? UpdatedBy { get; set; }
		public DateTime? UpdatedWhen { get; set; }
	}
}
