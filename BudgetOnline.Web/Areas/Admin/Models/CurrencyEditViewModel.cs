using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BudgetOnline.UI.Attributes;

namespace BudgetOnline.Web.Areas.Admin.Models
{
	public class CurrencyEditViewModel
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Display(Name = "Валюта")]
		[GridLayout(6)]
		[Required]
		public string Name { get; set; }

		//[MaxLength(1)]
		[Display(Name = "Символ")]
		[GridLayout(2)]
		public string Symbol { get; set; }

		[HiddenLabel]
		[Display(Name = "По-умолчанию")]
		public bool IsDefault { get; set; }

		[HiddenLabel]
		[Display(Name = "Заблокирована")]
		public bool IsDisabled { get; set; }

		[ScaffoldColumn(false)]
		[Display(Name = "Описание")]
		public string Description { get; set; }
	}
}