using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class UserPassword
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string MemoriableWord { get; set; }
		public string Password { get; set; }
		public bool IsDisabled { get; set; }
		public DateTime CreatedWhen { get; set; }
		public int CreatedBy { get; set; }
	}
}
