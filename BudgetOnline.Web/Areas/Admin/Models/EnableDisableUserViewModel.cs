namespace BudgetOnline.Web.Areas.Admin.Models
{
	public class EnableDisableUserViewModel
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public bool IsDisabled { get; set; }
	}
}