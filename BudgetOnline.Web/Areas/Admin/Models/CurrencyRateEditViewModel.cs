using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.UI.Attributes;
using BudgetOnline.UI.Models;
using BudgetOnline.UI.Models.SelectItems;
using BudgetOnline.Web.Infrastructure.Controls.Attributes;

namespace BudgetOnline.Web.Areas.Admin.Models
{
	public class CurrencyRateEditViewModel
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

        [UIHint("Date")]
		[Display(Name = "Дата")]
		[Required]
		public DateTime Date { get; set; }

        [Display(Name = "Из валюты")]
        [GridLayout(6)]
        [Required]
        public IdWithSelectList BaseCurrency { get; set; }

        [Display(Name = "В валюту")]
        [GridLayout(6)]
		[Required]
		public IdWithSelectList TargetCurrency { get; set; }

		[Display(Name = "Курс")]
		[GridLayout(2)]
		public decimal Rate { get; set; }

		[HiddenLabel]
		[Display(Name = "Заблокирована")]
		public bool IsDisabled { get; set; }

		public CurrencyRateEditViewModel()
		{
			Date = DateTime.Today;
			BaseCurrency = new IdWithSelectList { Id = 0, Items = new SelectItemsModel(Enumerable.Empty<SelectItemModel>()) };
			TargetCurrency = new IdWithSelectList { Id = 0, Items = new SelectItemsModel(Enumerable.Empty<SelectItemModel>()) };
		}
	}
}