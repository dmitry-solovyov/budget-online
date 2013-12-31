using System;
using System.Collections.Generic;
using System.ComponentModel;
using BudgetOnline.UI.Models.ViewCommands;

namespace BudgetOnline.Web.ViewModels
{
	public class TransactionListItemViewModel
	{
		public int Id { get; set; }
		[DisplayName("Дата")]
		public DateTime Date { get; set; }
		[DisplayName("Счет")]
		public string AccountSource { get; set; }
		public string AccountTarget { get; set; }

		[DisplayName("Валюта")]
		public string CurrencySource { get; set; }
		public string CurrencyTarget { get; set; }

		[DisplayName("Кол-во")]
		public decimal Amount { get; set; }
		[DisplayName("Сумма")]
		public decimal SumSource { get; set; }
		public decimal? SumTarget { get; set; }

		[DisplayName("Описание")]
		public string Description { get; set; }
		[DisplayName("Метки")]
		public string Tags { get; set; }

		public DateTime CreatedWhen { get; set; }
		public DateTime? UpdatedWhen { get; set; }
		public bool IsDisabled { get; set; }

		public IEnumerable<ViewCommandUIModel> Commands { get; set; }

		public TransactionListItemViewModel()
		{
			Commands = new BindingList<ViewCommandUIModel>();
		}
	}
}