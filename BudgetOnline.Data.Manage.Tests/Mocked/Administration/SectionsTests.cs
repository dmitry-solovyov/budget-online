using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Data.Manage.Tests.Mocked.Administration
{
	[TestClass]
	public class SectionsTests
	{
		private readonly Mock<ISectionRepository> _administrtion = new Mock<ISectionRepository>();

		private IEnumerable<Section> GenerateSections()
		{
			yield return new Section { Id = 1, Name = "name1" };
			yield return new Section { Id = 2, Name = "name2" };
			yield return new Section { Id = 3, Name = "name3" };
		}

		[TestInitialize]
		public void Setup()
		{
			_administrtion
				.Setup(o => o.GetSection(It.IsAny<Int32>()))
				.Returns((int sectionId) => GenerateSections().FirstOrDefault(o => o.Id == sectionId));

			_administrtion
				.Setup(o => o.GetSections())
				.Returns(GenerateSections());
		}

		[TestMethod]
		public void TestReadSections_FullList()
		{
			var a = _administrtion.Object;
			var result = a.GetSections().ToArray();

			Assert.AreEqual(3, result.Length);
			Assert.AreEqual(2, result[1].Id);

			Console.WriteLine(result[0].Name);
		}

		[TestMethod]
		public void TestReadSections_GetExistingSection()
		{
			var a = _administrtion.Object;
			var section = a.GetSection(1);

			Assert.IsNotNull(section);
			Assert.AreEqual(1, section.Id);

			Console.WriteLine(section.Name);
		}

		[TestMethod]
		public void TestReadSections_GetNonExistingSection()
		{
			var a = _administrtion.Object;
			var section = a.GetSection(-1);

			Assert.IsNull(section);
		}
	}
}
