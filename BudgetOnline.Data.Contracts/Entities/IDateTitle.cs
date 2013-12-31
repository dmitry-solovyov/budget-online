using System;

namespace BudgetOnline.Data.Contracts.Entities
{
	public interface IDateTitle
	{
		int Id { get; set; }
		int SectionId { get; set; }
		DateTime Date { get; set; }
		string Title { get; set; }

		int CreatedBy { get; set; }
		DateTime CreatedWhen { get; set; }
		int? UpdatedBy { get; set; }
		DateTime? UpdatedWhen { get; set; }
	}
}
