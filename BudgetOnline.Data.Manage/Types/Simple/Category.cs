using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class Category
	{
		public int Id { get; set; }
		public int SectionId { get; set; }
		public int? ParentId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsDefault { get; set; }
		public bool IsDisabled { get; set; }
		public int? CreatedBy { get; set; }
		public DateTime CreatedWhen { get; set; }
		public int? UpdatedBy { get; set; }
		public DateTime? UpdatedWhen { get; set; }
	}
}
