using System;

namespace BudgetOnline.Data.Manage.Types.Complex
{
	public class UserConnectInfo
	{
		public int UserId { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public bool IsDisabled { get; set; }
		public bool IsSectionAdmin { get; set; }
		public DateTime? LastConnected { get; set; }
	}
}
