using System;

namespace BudgetOnline.Data.Contracts.Entities
{
	public interface ITag
	{
		int Id { get; set; }
		int? SectionId { get; set; }
		string Name { get; set; }
		bool IsDisabled { get; set; }

		DateTime CreatedWhen { get; set; }
		int CreatedBy { get; set; }
		DateTime? UpdatedWhen { get; set; }
		int? UpdatedBy { get; set; }
	}
}
