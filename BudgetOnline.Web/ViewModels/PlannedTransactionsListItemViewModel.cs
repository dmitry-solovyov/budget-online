using System;
using System.Collections.Generic;
using System.ComponentModel;
using BudgetOnline.UI.Models.ViewCommands;

namespace BudgetOnline.Web.ViewModels
{
	public class PlannedTransactionsListItemViewModel
	{
		public int Id { get; set; }
		[DisplayName("Дата")]
		public DateTime FromDate { get; set; }
		[DisplayName("Дата")]
		public DateTime? ToDate { get; set; }

		[DisplayName("Счет")]
		public string Account { get; set; }

		[DisplayName("Период")]
		public string PeriodType { get; set; }


		[DisplayName("Валюта")]
		public string Currency { get; set; }

		[DisplayName("Кол-во")]
		public decimal Amount { get; set; }
		[DisplayName("Сумма")]
		public decimal Sum { get; set; }

		[DisplayName("Описание")]
		public string Description { get; set; }
		[DisplayName("Метки")]
		public string Tags { get; set; }

		public DateTime CreatedWhen { get; set; }
		public DateTime? UpdatedWhen { get; set; }
		public bool IsDisabled { get; set; }

		public IEnumerable<ViewCommandUIModel> Commands { get; set; }

		public PlannedTransactionsListItemViewModel()
		{
			Commands = new BindingList<ViewCommandUIModel>();
		}
	}
}