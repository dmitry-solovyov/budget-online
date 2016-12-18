using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BudgetOnline.UI.Attributes;

namespace BudgetOnline.Web.Areas.Admin.Models
{
	public class CategoryEditViewModel
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Display(Name = "Категория")]
		[GridLayout(6)]
		[Required]
		public string Name { get; set; }

		[UIHint("Multiline")]
		[Display(Name = "Описание")]
		public string Description { get; set; }

		[HiddenLabel]
		[Display(Name = "По-умолчанию")]
		public bool IsDefault { get; set; }

		[HiddenLabel]
		[Display(Name = "Блокировано")]
		public bool IsDisabled { get; set; }
	}
}