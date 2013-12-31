namespace BudgetOnline.Data.Contracts.Entities
{
	using System;

	public interface ICurrency
	{
		int Id { get; set; }
		int? SectionId { get; set; }
		string Name { get; set; }
		string Description { get; set; }

		bool IsDefault { get; set; }

		DateTime CreatedWhen { get; set; }
		int CreatedBy { get; set; }
		DateTime? UpdatedWhen { get; set; }
		int? UpdatedBy { get; set; }
	}
}
