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
	public class TransactionLinkRepositoryTests
	{
		private readonly Mock<ITransactionLinkRepository> _transactionLinkRepositoryMock = new Mock<ITransactionLinkRepository>();

		private IEnumerable<TransactionLink> GenerateTransactionLinks()
		{
			yield return new TransactionLink { Id = 98, ParentId = 1, ChildId = 2 };
			yield return new TransactionLink { Id = 99, ParentId = 3, ChildId = 4 };
		}

		[TestInitialize]
		public void Setup()
		{
			_transactionLinkRepositoryMock
				.Setup(o => o.GetByTransactionId(It.IsAny<int>()))
				.Returns((int transactionId) => GenerateTransactionLinks().FirstOrDefault(o => o.ChildId == transactionId || o.ParentId == transactionId));
		}

		[TestMethod]
		public void TestReadTransactionLinks_ExistingTransactionLinkByChild()
		{
			var a = _transactionLinkRepositoryMock.Object;
			var result = a.GetByTransactionId(4);

			Assert.AreEqual(99, result.Id);
		}

		[TestMethod]
		public void TestReadTransactionLinks_ExistingTransactionLinkByParent()
		{
			var a = _transactionLinkRepositoryMock.Object;
			var result = a.GetByTransactionId(1);

			Assert.AreEqual(98, result.Id);
		}

		[TestMethod]
		public void TestReadTransactionLinks_NotExistingTransactionLink()
		{
			var a = _transactionLinkRepositoryMock.Object;
			var result = a.GetByTransactionId(-1);

			Assert.IsNull(result);
		}
	}
}
