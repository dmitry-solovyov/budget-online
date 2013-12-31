using System.ComponentModel;

namespace BudgetOnline.Web.ViewModels
{
	public class ProfileViewModel
	{
		public int Id { get; set; }
		[ReadOnly(true)]
		public string Email { get; set; }
		public string Password { get; set; }
		public bool IsDisabled { get; set; }

		public bool AllowChangePassword { get; set; }
	}
}