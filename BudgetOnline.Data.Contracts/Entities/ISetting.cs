using System;

namespace BudgetOnline.Data.Contracts.Entities
{
	public interface ISetting
	{
		int Id { get; set; }
		int? SectionId { get; set; }
		int? UserId { get; set; }
		string Name { get; set; }
		string Value { get; set; }

		bool IsDisabled { get; set; }

		DateTime CreatedWhen { get; set; }
		int? CreatedBy { get; set; }
	}
}
