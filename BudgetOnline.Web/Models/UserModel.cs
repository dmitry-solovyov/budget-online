using System.Collections.Generic;
using System.Linq;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.Web.Infrastructure.Security;

namespace BudgetOnline.Web.Models
{
	public class UserModel
	{
		public int SectionId { get; set; }

		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string ContactPhoneNumber { get; set; }
		
		public bool IsDisabled { get; set; }

		public IEnumerable<UserPermission> Permissions { get; set; }
		public bool IsHasPermission(Roles role)
		{
			var roleId = role.GetRealId();
			return Permissions.Any(o => o.PermissionId == roleId);
		}

		public UserModel()
		{
			Permissions = Enumerable.Empty<UserPermission>();
		}
	}
}
