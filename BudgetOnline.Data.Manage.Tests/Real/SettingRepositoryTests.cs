using BudgetOnline.Data.Manage.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.Data.Manage.Tests.Real
{
	[TestClass]
	public class SettingRepositoryTests
	{
		[TestMethod]
		public void TestGetUsersSettings()
		{
			var repository = new SettingRepository();

			var items = repository.GetSettings(0, 0);

			Assert.IsNotNull(items);
		}

		[TestMethod]
		public void TestFindByName()
		{
			var repository = new SettingRepository();

			var item = repository.FindByName(0, 0, "setting");

			Assert.IsNull(item);
		}
	}
}
