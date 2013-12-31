namespace BudgetOnline.Data.Contracts.Entities
{
	using System;

	public interface ICategory
	{
		int Id { get; set; }
		int SectionId { get; set; }
		int? ParentId { get; set; }
		string Name { get; set; }
		string Description { get; set; }

		bool IsDisabled { get; set; }

		int? CreatedBy { get; set; }
		DateTime CreatedWhen { get; set; }
		int? UpdatedBy { get; set; }
		DateTime? UpdatedWhen { get; set; }
	}
}
