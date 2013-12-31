using System;

namespace BudgetOnline.Data.Contracts.Entities
{
	public interface ISection
	{
		int Id { get; set; }
		string Name { get; set; }
		string Description { get; set; }
		bool IsDisabled { get; set; }

		DateTime CreatedWhen { get; set; }
		int CreatedBy { get; set; }
		DateTime? UpdatedWhen { get; set; }
		int? UpdatedBy { get; set; }
	}
}
