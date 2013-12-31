using System;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.Web.Infrastructure.Attributes;

namespace BudgetOnline.Web.Infrastructure.Security
{
	public class RestrictRoleAttribute : AuthorizeAttribute
	{
		private readonly Roles _roles;

		public bool HasRole(Roles role)
		{
			return (_roles & role) != 0;
		}

		public RestrictRoleAttribute(Roles roles)
		{
			_roles = roles;

			var result = (
					from Roles role in Enum.GetValues(typeof (Roles)) 
					where (roles & role) != 0 
					select role.ToString()
				).ToList();

			Roles = string.Join(" ", result);
		}
	}

	[Flags]
	public enum Roles
	{
		[RowId(1)]
		SystemAdmin = 1,
		[RowId(2)]
		SectionAdmin = 2,
		[RowId(3)]
		FactAdd = 4,
		[RowId(4)]
		FactView = 8,
		[RowId(5)]
		PlanAdd = 16,
		[RowId(6)]
		PlanView = 32,
		[RowId(7)]
		Statistics = 64,
	}

	public static class RolesExtensions
	{
		public static int GetRealId(this Roles roles)
		{
			var availableRoles = Enum.GetValues(typeof (Roles));

			return (
					from Roles availableRole in availableRoles 
					where (availableRole & roles) != 0 let type = typeof (Roles) 
					select type.GetMember(availableRole.ToString()) into memInfo 
					select memInfo[0].GetCustomAttributes(typeof (RowIdAttribute), false) 
					into attributes 
					where attributes.Length > 0 
					select ((RowIdAttribute) attributes[0]).RowId
				).FirstOrDefault();
		}
	}
}