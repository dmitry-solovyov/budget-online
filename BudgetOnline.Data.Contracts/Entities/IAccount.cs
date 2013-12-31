using System;

namespace BudgetOnline.Data.Contracts.Entities
{
	public interface IAccount
	{
		int Id { get; set; }
		int SectionId { get; set; }
		bool IsDisabled { get; set; }

		string Name { get; set; }
		string Description { get; set; }

		bool ShowForIncome { get; set; }
		bool ShowForOutcome { get; set; }
		bool ShowForTransfer { get; set; }

		DateTime CreatedWhen { get; set; }
		int CreatedBy { get; set; }
		DateTime? UpdatedWhen { get; set; }
		int? UpdatedBy { get; set; }
	}
}
