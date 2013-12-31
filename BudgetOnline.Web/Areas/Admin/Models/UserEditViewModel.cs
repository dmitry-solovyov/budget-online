using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BudgetOnline.Web.Models;

namespace BudgetOnline.Web.Areas.Admin.Models
{
	public class UserEditViewModel
	{
		[ScaffoldColumn(false)]
		public int SectionId { get; set; }

		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }
		[DisplayName("Имя")]
		[Required]
		public string Name { get; set; }

		[DisplayName("E-mail")]
		[Required]
		public string Email { get; set; }
		
		[DisplayName("Номер тел.")]
		public string ContactPhoneNumber { get; set; }
		
		[ScaffoldColumn(false)]
		public bool IsReadOnly { get; set; }
		[ScaffoldColumn(false)]
		public bool IsDisabled { get; set; }
		[ScaffoldColumn(false)]
		public bool IsSectionAdmin { get; set; }

		[DisplayName("Права")]
		[UIHint("ListWithMultiSelects")]
		public ListWithMultiSelects Permissions { get; set; }

		public UserEditViewModel()
		{
			Permissions = new ListWithMultiSelects();
		}
	}
}
