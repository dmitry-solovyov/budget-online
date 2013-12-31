using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class SectionAdmin
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int SectionId { get; set; }
		public DateTime CreatedWhen { get; set; }
		public int CreatedBy { get; set; }
		public DateTime BlockedWhen { get; set; }
		public int BlockedBy { get; set; }
	}
}
