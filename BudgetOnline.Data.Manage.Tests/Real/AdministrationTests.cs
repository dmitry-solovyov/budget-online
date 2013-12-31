using System;
using System.Linq;
using BudgetOnline.Data.Manage.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.Data.Manage.Tests.Real
{
	[TestClass]
	public class AdministrationTests
	{
		[TestMethod]
		public void TestReadUsers()
		{
			var userRepository = new UserRepository();
			var sectionRepository = new SectionRepository();

			var sections = sectionRepository.GetSections();

			var users = userRepository.GetUsers(sections.First().Id).ToArray();

			Assert.IsNotNull(users[0].Email);
			Assert.IsTrue(users[0].Id> 0);

			Console.WriteLine(users[0].Email);
		}
	}
}
