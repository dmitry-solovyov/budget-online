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
	public class UserPasswordRepositoryTests
	{
		private const string Pass1 = "pass1";
		private const string Pass2 = "pass2";
		private const string Pass3 = "pass3";

		private readonly Mock<IUserPasswordRepository> _userPasswordRepository = new Mock<IUserPasswordRepository>();
		private readonly User _user1 = new User
										{
											Id = 1,
											Email = "email1"
										};
		private readonly User _user2 = new User
										{
											Id = 2,
											Email = "email2"
										};

		private IEnumerable<UserPassword> GenerateUserPasswords()
		{
			yield return new UserPassword { Id = 1, UserId = _user1.Id, MemoriableWord = "word1", Password = Pass1, CreatedWhen = DateTime.Now.AddDays(-1), IsDisabled = true };
			yield return new UserPassword { Id = 2, UserId = _user1.Id, MemoriableWord = "word2", Password = Pass2, CreatedWhen = DateTime.Now };
			yield return new UserPassword { Id = 3, UserId = _user1.Id, MemoriableWord = "word3", Password = Pass3, CreatedWhen = DateTime.Now.AddDays(-1) };
			yield return new UserPassword { Id = 4, UserId = _user2.Id, MemoriableWord = "word4", Password = "", CreatedWhen = DateTime.Now };
		}

		[TestInitialize]
		public void Setup()
		{
			_userPasswordRepository
				.Setup(o => o.GetPasswords(It.IsAny<int>()))
				.Returns((int userId) => GenerateUserPasswords().Where(o => o.UserId == userId && !o.IsDisabled).OrderBy(o => o.CreatedWhen));
		}

		[TestMethod]
		public void GetPasswords_ShouldReturn2Rows_WhenMethodCalled()
		{
			var a = _userPasswordRepository.Object;
			var result = a.GetPasswords(_user1.Id);

			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public void GetPasswords_ShouldExcludeDisabledPasswords_WhenExistDisabledPassword()
		{
			var a = _userPasswordRepository.Object;
			var result = a.GetPasswords(_user1.Id).ToArray();

			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}
		
		[TestMethod]
		public void GetPasswords_ShouldReturnOrderedRows_WhenMethodCalled()
		{
			var a = _userPasswordRepository.Object;
			var result = a.GetPasswords(_user1.Id).ToArray();

			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			
			Assert.AreEqual(3, result[0].Id);
			Assert.AreEqual(2, result[1].Id);
		}
	}
}
