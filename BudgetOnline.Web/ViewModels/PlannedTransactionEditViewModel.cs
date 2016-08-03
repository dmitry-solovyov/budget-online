using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BudgetOnline.Common.Enums;
using BudgetOnline.UI.Attributes;
using BudgetOnline.UI.Models;
using BudgetOnline.UI.Models.Editors;
using BudgetOnline.Web.UI.Validators;

namespace BudgetOnline.Web.ViewModels
{
	public class PlannedTransactionEditViewModel
	{
		public PlannedTransactionEditViewModel()
		{
			Amount = 1m;
			FromDate = DateTime.Now.Date;
			Sum = new CurrencyBundle();
			Category = new IdWithSelectList();
			PeriodType = new IdWithSelectList();
			TransactionType = new IdWithSelectList { Id = (int)TransactionTypes.Expense };
		}

		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[HiddenInput(DisplayValue = false)]
		public int? LinkedId { get; set; }


		[UIHint("Date")]
		[DataType(DataType.Date)]
		[DisplayName("От даты")]
		[Display(Order = 1)]
		[YearRange(FromYear = 2011, ErrorMessage = "Must be after 01.01.2011")]
		public DateTime FromDate { get; set; }

		[UIHint("Date")]
		[DataType(DataType.Date)]
		[DisplayName("До")]
		[Display(Order = 1)]
		[YearRange(FromYear = 2011, ErrorMessage = "Must be after 01.01.2011")]
		public DateTime? ToDate { get; set; }

		[DisplayName("Период")]
		[Display(Order = 2)]
		public IdWithSelectList PeriodType { get; set; }

		[DisplayName("Тип")]
		[Display(Order = 3)]
		public IdWithSelectList TransactionType { get; set; }

		[DisplayName("Категория")]
		[Display(Order = 4)]
		public IdWithSelectList Category { get; set; }


		[ScaffoldColumn(false)]
		[Range(1, double.MaxValue)]
		public decimal Amount { get; set; }


		[DisplayName("Счет")]
		[Display(Order = 5, GroupName = "Money")]
		public CurrencyBundle Sum { get; set; }

		[DisplayName("Описание")]
		[UIHint("Multiline")]
		public string Description { get; set; }

		[UIHint("Autocomplete")]
		[AutocompleteOption("GetTags", "Transactions")]
		[DisplayName("Тэги")]
		public string Tags { get; set; }
		[HiddenInput(DisplayValue = false)]
		public int[] TagIds { get; set; }


		[HiddenLabel]
		[DisplayName("Не учитывать")]
		public bool IsDisabled { get; set; }

		[HiddenLabel]
		[ScaffoldColumn(false)]
		[DisplayName("Продолжить добавление")]
		public bool IsCreateNewAfterSave { get; set; }

		[ScaffoldColumn(false)]
		public DateTime CreatedWhen { get; set; }
		[ScaffoldColumn(false)]
		public int CreatedBy { get; set; }

		[ScaffoldColumn(false)]
		public DateTime? UpdatedWhen { get; set; }
		[ScaffoldColumn(false)]
		public int? UpdatedBy { get; set; }
	}
}