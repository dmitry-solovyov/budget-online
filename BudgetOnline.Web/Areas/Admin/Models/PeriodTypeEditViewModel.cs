using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BudgetOnline.UI.Attributes;

namespace BudgetOnline.Web.Areas.Admin.Models
{
	public class PeriodTypeEditViewModel
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Display(Name = "Период")]
		[GridLayout(6)]
		[Required]
		public string Name { get; set; }

		[HiddenLabel]
		[Display(Name = "Заблокирован")]
		public bool IsDisabled { get; set; }
	}
}