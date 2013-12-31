using System;
using System.Collections.Generic;
using System.Linq;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Data.Manage.Tests.Mocked.Repositories
{
	[TestClass]
	public class UserRepositoryTests
	{
		private const int SectionId = 1;
		private readonly Mock<IUserRepository> _administrtionMock = new Mock<IUserRepository>();

		private IEnumerable<User> GenerateUsers()
		{
			yield return new User { Id = 1, SectionId = 1, Email = "name1@email.com", Name = "name1" };
			yield return new User { Id = 2, SectionId = 1, Email = "name2@email.com", Name = "name2" };
			yield return new User { Id = 3, SectionId = 2, Email = "name3@email.com", Name = "name3" };
		}

		[TestInitialize]
		public void Setup()
		{
			_administrtionMock
				.Setup(o => o.GetUser(It.IsAny<int>()))
				.Returns((int userId) => GenerateUsers().FirstOrDefault(o => o.Id == userId));

			_administrtionMock
				.Setup(o => o.GetUsers(It.IsAny<int>()))
				.Returns((int sectionId) => GenerateUsers().Where(o => o.SectionId == sectionId));

			//_administrtionMock
			//    .Setup(o => o.GetPasswords(It.IsAny<int>()))
			//    .Returns((int userId) => GeneratePasswords().Where(o => o.UserId == userId));
		}

		[TestMethod]
		public void TestReadUsers_ExistingUser()
		{
			var a = _administrtionMock.Object;
			var result = a.GetUsers(SectionId).ToArray();

			Assert.AreEqual(2, result.Length);
			Assert.AreEqual(2, result[1].Id);

			Console.WriteLine(result[0].Email);
		}

		[TestMethod]
		public void TestReadUsers_NotExistingUser()
		{
			var a = _administrtionMock.Object;
			var result = a.GetUsers(-1).ToArray();

			Assert.AreEqual(0, result.Length);
		}

		[TestMethod]
		public void TestReadUsers_GetExistingUser()
		{
			var a = _administrtionMock.Object;
			var result = a.GetUser(3);

			Assert.IsNotNull(result);
			Assert.AreEqual("name3", result.Name);
		}


		[TestMethod]
		public void TestReadUsers_GetNotExistingUser()
		{
			var a = _administrtionMock.Object;
			var result = a.GetUser(-3);

			Assert.IsNull(result);
		}

		[TestMethod]
		public void Administration()
		{
			var a = _administrtionMock.Object;
			var result = a.FindByEmail("");

			Assert.IsNull(result);
		}
	}
}
