using System.Linq;
using BudgetOnline.Web.Infrastructure.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.Web.Tests.Authentication
{
	[TestClass]
	public class RestrictRoleAttributeTests
	{
		[TestMethod]
		public void TestRolesForReader()
		{
			var attr = GetRestrictRoleAttributeReader();

			var roles = attr.Roles.Split(' ');

			Assert.AreEqual(2, roles.Length);
			Assert.IsTrue(roles.Contains("FactView"));
			Assert.IsTrue(roles.Contains("PlanView"));
		}

		[TestMethod]
		public void TestRolesForWriter()
		{
			var attr = GetRestrictRoleAttributeWriter();

			var roles = attr.Roles.Split(' ');

			Assert.AreEqual(4, roles.Length);
			Assert.IsTrue(roles.Contains("FactView"));
			Assert.IsTrue(roles.Contains("FactAdd"));
			Assert.IsTrue(roles.Contains("PlanView"));
			Assert.IsTrue(roles.Contains("PlanAdd"));
		}

		[TestMethod]
		public void TestRolesForSysAdmin()
		{
			var attr = GetRestrictRoleAttributeSysAdmin();

			var roles = attr.Roles.Split(' ');

			Assert.AreEqual(1, roles.Length);
			Assert.IsTrue(roles.Contains("SystemAdmin"));
		}

		[TestMethod]
		public void TestRolesForWriter_HasRole()
		{
			var attr = GetRestrictRoleAttributeWriter();

			Assert.IsTrue(attr.HasRole(Roles.FactView));
			Assert.IsTrue(attr.HasRole(Roles.FactAdd));
			Assert.IsTrue(attr.HasRole(Roles.PlanView));
			Assert.IsTrue(attr.HasRole(Roles.PlanAdd));
			Assert.IsFalse(attr.HasRole(Roles.SectionAdmin));
			Assert.IsFalse(attr.HasRole(Roles.SystemAdmin));
		}

		private RestrictRoleAttribute GetRestrictRoleAttributeReader()
		{
			return new RestrictRoleAttribute(Roles.FactView | Roles.PlanView);
		}

		private RestrictRoleAttribute GetRestrictRoleAttributeWriter()
		{
			return new RestrictRoleAttribute(Roles.FactView | Roles.FactAdd | Roles.PlanView | Roles.PlanAdd);
		}

		private RestrictRoleAttribute GetRestrictRoleAttributeSysAdmin()
		{
			return new RestrictRoleAttribute(Roles.SystemAdmin);
		}

	}
}
