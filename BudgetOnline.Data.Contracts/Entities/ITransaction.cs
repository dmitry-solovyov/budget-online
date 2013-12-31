using System;

namespace BudgetOnline.Data.Contracts.Entities
{
	public interface ITransaction
	{
		int Id { get; set; }
		int? LinkedId { get; set; }
		bool? LinkedAsParent { get; set; }
		int TransactionTypeId { get; set; }
		int SectionId { get; set; }
		int CurrencyId { get; set; }
		int? CategoryId { get; set; }
		DateTime Date { get; set; }
		int AccountId { get; set; }
		decimal Amount { get; set; }
		decimal Sum { get; set; }
		string Formula { get; set; }
		string Tags { get; set; }
		string Description { get; set; }
		bool IsDisabled { get; set; }
		bool IsNotForStatistics { get; set; }

		int CreatedBy { get; set; }
		DateTime CreatedWhen { get; set; }
		int? UpdatedBy { get; set; }
		DateTime? UpdatedWhen { get; set; }
	}
}
