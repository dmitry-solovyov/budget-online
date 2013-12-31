using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Repositories;
using BudgetOnline.Data.Manage.Types.Complex;
using BudgetOnline.Data.Manage.Types.Simple;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Linq;

namespace BudgetOnline.Data.Manage.Tests.Mocked.Repositories
{
    [TestClass]
    public class TransactionRepositoryTests
    {
        private readonly Mock<MSSQL.BudgetOnlineDBDataContext> _contextMock = new Mock<MSSQL.BudgetOnlineDBDataContext>();
        private readonly Mock<Table<Transaction>> _transactionTableMock = new Mock<Table<Transaction>>();

        private IEnumerable<Transaction> GenerateTransactions()
        {
            yield return
                new Transaction
                    {
                        Id = 98,
                        AccountId = 1,
                        Amount = 1,
                        CategoryId = 2,
                        CurrencyId = 3,
                        Date = DateTime.Now.Date,
                        SectionId = 1,
                        Sum = 101m,
                        TransactionTypeId = 1
                    };
            yield return
                new Transaction
                    {
                        Id = 99,
                        AccountId = 1,
                        Amount = 1,
                        CategoryId = 2,
                        CurrencyId = 3,
                        Date = DateTime.Now.Date,
                        SectionId = 1,
                        Sum = 101m,
                        TransactionTypeId = 1
                    };
        }

        [TestInitialize]
        public void Setup()
        {
            
        }

        ////[TestMethod]
        //public void TestInsertLinkedTransactions_ExistingTransactionByChild()
        //{
        //    var a = _transactionRepositoryMock.Object;

        //    var initial = GenerateTransactions().ElementAt(0);
        //    var result = a.Insert(initial);

        //    Assert.AreEqual(initial.AccountId, result.AccountId);
        //    Assert.AreEqual(initial.CategoryId, result.CategoryId);
        //    Assert.AreEqual(initial.CurrencyId, result.CurrencyId);
        //    Assert.AreEqual(initial.Date, result.Date);
        //    Assert.AreEqual(initial.Description, result.Description);
        //}

        ////[TestMethod]
        //public void GetListTotals_ShouldReturn_()
        //{
        //    var a = _transactionRepositoryMock.Object;

        //    var initial = GenerateTransactions().ElementAt(0);
        //    var result = a.Insert(initial);

        //    Assert.AreEqual(initial.AccountId, result.AccountId);
        //    Assert.AreEqual(initial.CategoryId, result.CategoryId);
        //    Assert.AreEqual(initial.CurrencyId, result.CurrencyId);
        //    Assert.AreEqual(initial.Date, result.Date);
        //    Assert.AreEqual(initial.Description, result.Description);
        //}

        private ITransactionRepository GetRepository()
        {
            return new TransactionRepository
                       { 
                           
                       };
        }


        private Mock<MetaTable> SetupMetaTableMock()
        {
            var mock = new Mock<MetaTable>();

            return mock;
        }
    }
}
