using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BudgetOnline.UI.Attributes;
using BudgetOnline.Web.Infrastructure.Controls.Attributes;

namespace BudgetOnline.Web.Areas.Admin.Models
{
	public class AccountEditViewModel
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Display(Name = "Счет")]
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
		[Display(Name = "Заблокирован")]
		public bool IsDisabled { get; set; }

		[HiddenLabel]
		[Display(Name = "Внешний счет")]
        [QuickInfo(Name = "Внешний счет", HelpUrl = "")]
		public bool IsExternal { get; set; }

		[HiddenLabel]
        [Display(Name = "Для заходов")]
        public bool ShowForIncome { get; set; }

		[HiddenLabel]
        [Display(Name = "Для расходов")]
        public bool ShowForOutcome { get; set; }

		[HiddenLabel]
        [Display(Name = "Для переводов")]
        public bool ShowForTransfer { get; set; }
	}
}