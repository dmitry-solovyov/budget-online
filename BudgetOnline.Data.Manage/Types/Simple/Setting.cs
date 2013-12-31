using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class Setting 
	{
		public int Id { get; set; }
		public int? SectionId { get; set; }
		public int? UserId { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
		public bool IsDisabled { get; set; }
		public DateTime CreatedWhen { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime? UpdatedWhen { get; set; }
		public int? UpdateddBy { get; set; }
	}
}
