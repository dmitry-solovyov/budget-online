using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BudgetOnline.UI.Attributes;

namespace BudgetOnline.Web.ViewModels.Security
{
	public class LogInViewModel
	{
		[Required]
		[HiddenLabel]
		[Placeholder("Email")]
		[DataType(DataType.EmailAddress)]
		public string UserName { get; set; }
		
		[Required]
		[HiddenLabel]
		[Placeholder("Пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DisplayName("Запомнить меня")]
		[HiddenLabel]
		public bool RememberMe { get; set; }
	}
}