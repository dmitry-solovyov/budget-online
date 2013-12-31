using System;

namespace BudgetOnline.Data.Manage.Types.Simple
{
	public class UserPermission
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int PermissionId { get; set; }

		public int CreatedBy { get; set; }
		public DateTime CreatedWhen { get; set; }
	}
}
