using System;

namespace BudgetOnline.Data.Contracts.Entities
{
	public interface ITransactionTag
	{
		int Id { get; set; }
		int SectionId { get; set; }
		int TransactionId { get; set; }
		int? TagId { get; set; }
		string Tag { get; set; }
		bool IsDisabled { get; set; }

		DateTime CreatedWhen { get; set; }
		int CreatedBy { get; set; }
		DateTime? UpdatedWhen { get; set; }
		int? UpdatedBy { get; set; }
	}
}
