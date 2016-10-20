using System;
using System.Linq;

namespace BudgetOnline.Web.Security
{
	public sealed class CustomRoleProvider : RoleProvider
	{
		public ILogWriter Log = DependencyResolver.Current.GetService<ILogWriter>();
		public IMembershipHelper MembershipHelper = DependencyResolver.Current.GetService<IMembershipHelper>();

		#region Overrides of RoleProvider

		public override bool IsUserInRole(string username, string roleName)
		{
			Log.TraceFormat("IsUserInRole for username={0}, rolename={1}", username, roleName);

			var user = MembershipHelper.GetUser(username);

			Roles parsedRole;
			if(Enum.TryParse(roleName, true, out parsedRole))
			{
				var result = user.IsHasPermission(parsedRole);
				Log.TraceFormat("IsUserInRole for username={0}, rolename={1}, result={2}", username, roleName, result);
				return result;
			}

			return false;
		}

		public override string[] GetRolesForUser(string username)
		{
			Log.TraceFormat("GetRolesForUser for user={0}", username);

			var user = MembershipHelper.GetUser(username);
			if(user != null)
			{
				Roles result = 0;
				var availableRoles = Enum.GetValues(typeof(Roles));

				if (!user.IsDisabled)
				{
					//result = Roles.SystemAdmin | Roles.SectionAdmin | Roles.FactAdd | Roles.FactView | Roles.PlanAdd | Roles.PlanView | Roles.Statistics; //TODO: remove tests line

					if (user.IsHasPermission(Roles.SystemAdmin))
						result = Roles.SystemAdmin | Roles.SectionAdmin | Roles.FactAdd | Roles.FactView | Roles.PlanAdd | Roles.PlanView | Roles.Statistics;
					else if (user.IsHasPermission(Roles.SectionAdmin))
						result = Roles.SectionAdmin | Roles.FactAdd | Roles.FactView | Roles.PlanAdd | Roles.PlanView | Roles.Statistics;
					else
					{
						var foundRoles = (
											from Roles availableRole in availableRoles
											where user.IsHasPermission(availableRole)
											select availableRole
										 );

						result = foundRoles.Aggregate(result, (current, role) => (current | role));
					}
				}

				var listOfRoles = (
						from Roles availableRole in availableRoles 
						where (result & availableRole) != 0 
						select availableRole.ToString()
					).ToArray();

				Log.TraceFormat("GetRolesForUser for user={0}. Found roles: {1}", username, string.Join(", ", listOfRoles));

				return listOfRoles;
			}

			return new string[0];
		}

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new NotImplementedException();
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string ApplicationName { get; set; }

		#endregion
	}
}