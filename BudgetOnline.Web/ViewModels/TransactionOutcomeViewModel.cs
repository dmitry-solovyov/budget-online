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
	public class TransactionOutcomeViewModel
	{
		public TransactionOutcomeViewModel()
		{
			Amount = 1m;
			SumBundle = new CurrencyBundle();
			Category = new IdWithSelectList();
			Date = DateTime.Now.Date;
			TransactionType = new IdWithSelectList { Id = (int)TransactionTypes.Expense };
		}

		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[HiddenInput(DisplayValue = false)]
		public int? LinkedId { get; set; }




		[UIHint("Date")]
		[DataType(DataType.Date)]
		[DisplayName("Дата")]
		[Display(Order = 1)]
		[YearRange(FromYear = 2011, ErrorMessage = "Must be after 01.01.2011")]
		public DateTime Date { get; set; }

		[ScaffoldColumn(false)]
		public IdWithSelectList TransactionType { get; set; }

		[DisplayName("Категория")]
		[Display(Order = 3)]
		public IdWithSelectList Category { get; set; }

		[ScaffoldColumn(false)]
		[Range(1, double.MaxValue)]
		public decimal Amount { get; set; }

		[DisplayName("Сумма")]
		[Display(Order = 4, GroupName = "Money")]
		public CurrencyBundle SumBundle { get; set; }

		//[DisplayName("Валюта")]
		//public IdWithSelectList Currency { get; set; }

		[DisplayName("Описание")]
		[UIHint("Multiline")]
		public string Description { get; set; }

		[DisplayName("Тэги")]
		public string Tags { get; set; }
		[HiddenInput(DisplayValue = false)]
		public int[] TagIds { get; set; }

		[HiddenLabel]
		public bool IsDisabled { get; set; }

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