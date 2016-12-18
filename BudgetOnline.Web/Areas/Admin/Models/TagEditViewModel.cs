using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BudgetOnline.UI.Attributes;

namespace BudgetOnline.Web.Areas.Admin.Models
{
	public class TagEditViewModel
	{
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Display(Name = "Метка")]
		[GridLayout(6)]
		[Required]
		public string Name { get; set; }

		[HiddenLabel]
		[Display(Name = "Заблокирована")]
		public bool IsDisabled { get; set; }

        [Display(Name = "Ссылок")]
		[ReadOnly(true)]
        public int Hits { get; set; }
	}
}