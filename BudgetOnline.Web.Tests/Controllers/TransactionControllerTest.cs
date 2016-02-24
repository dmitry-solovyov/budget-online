using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.BusinessLayer.Contracts;
using BudgetOnline.Common.Enums;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Complex;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.UI.Models;
using BudgetOnline.UI.Models.Editors;
using BudgetOnline.Web.Controllers;
using BudgetOnline.Web.Infrastructure.Core;
using BudgetOnline.Web.Infrastructure.Helpers;
using BudgetOnline.Web.Infrastructure.Security;
using BudgetOnline.Web.Models;
using BudgetOnline.Web.Tests.Helpers;
using BudgetOnline.Web.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Web.Tests.Controllers
{
	[TestClass]
	public class TransactionControllerTest
	{
		private readonly Mock<ITransactionRepository> _transactionRepositoryMock = new Mock<ITransactionRepository>();
		private readonly Mock<ITransactionTagRepository> _transactionTagRepositoryMock = new Mock<ITransactionTagRepository>();
		private readonly Mock<ITransactionLinkRepository> _transactionLinkRepositoryMock = new Mock<ITransactionLinkRepository>();
		private readonly Mock<ICurrencyRepository> _currencyRepositoryMock = new Mock<ICurrencyRepository>();
		private readonly Mock<IAccountRepository> _accountRepositoryMock = new Mock<IAccountRepository>();
		private readonly Mock<ICategoryRepository> _categoryRepositoryMock = new Mock<ICategoryRepository>();
		private readonly Mock<MembershipHelper> _membershipHelper = new Mock<MembershipHelper>();
		private readonly Mock<ITransactionDataHelper> _transactionDataHelperMock = new Mock<ITransactionDataHelper>();
		private readonly Mock<IDictionaries> _dictionariesMock = new Mock<IDictionaries>();

		private const int DefaultCurrencyId = 2;
		private const int DefaultAccountIdOut = 3;
		private const int DefaultAccountIdIn = 33;
		private const int DefaultCategoryId = 4;
		private const int SectionId = 1;
		private Transaction _transacion;
		private Transaction _transacionFromOtherSection;

		private readonly string[] _defailtTags = new[] { "Tag1", "Tag 2" };
		private IEnumerable<TransactionTag> TransactionTags
		{
			get
			{
				return new[] {
					new TransactionTag { Id = 1, SectionId = SectionId, Tag = _defailtTags[0], TransactionId = _transacion.Id},
					new TransactionTag { Id = 2, SectionId = SectionId, Tag = _defailtTags[1], TransactionId = _transacion.Id}
				};

			}
		}

		private readonly UserModel _currentUser =
			new UserModel
			{
				Id = 1,
				SectionId = SectionId,
			};
		private readonly UserModel _createdUser =
			new UserModel
			{
				Id = 2,
				SectionId = SectionId,
			};
		private readonly UserModel _createdUserFromOtherSection =
			new UserModel
			{
				Id = 3,
				SectionId = SectionId + 99,
			};

		[TestInitialize]
		public void Setup()
		{
			_transacion = new Transaction
			{
				Id = 12345,
				AccountId = DefaultAccountIdOut,
				CurrencyId = DefaultCurrencyId,
				CategoryId = DefaultCategoryId,
				SectionId = SectionId,
				TransactionTypeId = (int)TransactionTypes.Income,
				Amount = 1m,
				Sum = 123m,
				Formula = "100+23",
				CreatedBy = _createdUser.Id,
				Date = DateTime.UtcNow.Date,
				IsDisabled = false,
				LinkedId = null,
				Description = "Description",
				Tags = string.Join(", ", _defailtTags),
				CreatedWhen = DateTime.UtcNow,
			};


			_transacionFromOtherSection = new Transaction
			{
				Id = 2,
				Amount = 1m,
				Sum = 120m,
				CreatedBy = _createdUserFromOtherSection.Id,
				Date = DateTime.UtcNow.Date
			};


			_currencyRepositoryMock
				.Setup(o => o.GetList(It.IsAny<int>()))
				.Returns(new[]
				         	{
				         		new Currency { Id = 1, Name = "Currency 1" },
								new Currency { Id = DefaultCurrencyId, Name = "Currency 2", IsDefault = true},
				         	}.AsQueryable());

			_categoryRepositoryMock
				.Setup(o => o.GetList(It.IsAny<int>()))
				.Returns(new[]
				         	{
				         		new Category { Id = 1, SectionId = SectionId, Name = "Category 1" },
								new Category { Id = DefaultCategoryId, SectionId = SectionId, Name = "Category 2", IsDefault = true},
				         	}.AsQueryable());

			_accountRepositoryMock
				.Setup(o => o.GetList(It.IsAny<int>()))
				.Returns(new[]
				         	{
				         		new Account { Id = 1, Name = "Account 1" },
								new Account { Id = DefaultAccountIdOut, Name = "Account 2", IsDefault = true},
				         	}.AsQueryable());

			_transactionTagRepositoryMock
				.Setup(o => o.GetByTransaction(It.Is<int>(t => t == _transacion.Id)))
				.Returns(TransactionTags.AsQueryable());


			_membershipHelper
				.Setup(o => o.GetUser())
				.Returns(_currentUser);

			_membershipHelper
				.Setup(o => o.UsersInOneSection(It.Is<int?[]>(p => p.Length == 2 && p[0] == _currentUser.Id && p[1] == _createdUser.Id)))
				.Returns(true);
		}

		//[TestMethod]
		public void ListView_ShouldReturnOnlyRowsInSection_WhenRequestedList()
		{

		}

		#region Edit GET

		[TestMethod]
		public void EditGet_ShouldReturnHttpNotFoundReposnse_WhenRowDoesntExist()
		{
			var controller = GetTransactionController();

			var result = controller.Edit(-1);

			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}

		[TestMethod]
		public void EditGet_ShouldReturnHttpNotFoundReposnse_WhenRowDoesntBelongsToSameSection()
		{
			var controller = GetTransactionController();

			var result = controller.Edit(_transacionFromOtherSection.Id);

			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}

		[TestMethod]
		public void EditGet_ShouldReturnPositiveSum_WhenOutcomeLoaded()
		{
			var tr = GetSimpleTransaction(TransactionTypes.Expense);

			var controller = GetTransactionController(tr);

			var result = controller.Edit(_transacion.Id);

			var model = ((ViewResult)result).Model as TransactionEditViewModel;

			AssertOnGetEdit(tr, model);
		}

		[TestMethod]
		public void EditGet_ShouldSumFieldAccordingTotype_WhenIncome()
		{
			var tr = GetSimpleTransaction();
			tr.TransactionTypeId = (int)TransactionTypes.Income;

			var controller = GetTransactionController(tr);

			var result = controller.Edit(_transacion.Id);

			var model = ((ViewResult)result).Model as TransactionEditViewModel;

			AssertOnGetEdit(tr, model);
		}

		[TestMethod]
		public void EditGet_ShouldSumFieldAccordingTotype_WhenOutcome()
		{
			var tr = GetSimpleTransaction();
			tr.TransactionTypeId = (int)TransactionTypes.Expense;

			var controller = GetTransactionController(tr);

			var result = controller.Edit(_transacion.Id);

			var model = ((ViewResult)result).Model as TransactionEditViewModel;

			AssertOnGetEdit(tr, model);
		}

	    [TestMethod] 
        public void EditGet_ShouldRecoverLinkedRecord_WhenTransferRecordDoesntHasRowInDB()
        {
            var tr = GetSimpleTransaction();
            tr.TransactionTypeId = (int)TransactionTypes.Transfer;

            var controller = GetTransactionController(tr);

            var result = controller.Edit(_transacion.Id);

            var model = ((ViewResult)result).Model as TransactionEditViewModel;

            Assert.AreEqual(Math.Abs(model.SumOut.Sum), Math.Abs(model.SumIn.Sum)); 
        }

		#endregion


		#region Edit POST

		[TestMethod]
		public void EditPost_ShouldRedirectToList_WhenCorrectTransactionPosted()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleEditModel();

			var result = controller.Edit(initialModel);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

			var redirectResult = (RedirectToRouteResult)result;

			Assert.AreEqual("list", redirectResult.RouteValues["action"], "Should redirect to list");
		}

		[TestMethod]
		public void EditPost_ShouldUpdateToTransactionRepository_WhenCorrectTransactionPosted()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleEditModel();

			var result = controller.Edit(initialModel);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

			_transactionRepositoryMock.Verify(
				o => o.Update(It.Is<Transaction>(
					t => t.Amount == initialModel.Amount
						&& t.IsDisabled == initialModel.IsDisabled
						&& t.Formula == initialModel.SumIn.Formula
						&& t.Sum == initialModel.SumIn.Sum
						&& t.Id == initialModel.Id
						&& t.Tags == initialModel.Tags
						&& t.LinkedId == initialModel.LinkedId
						&& t.CurrencyId == initialModel.SumIn.Currency.Id
						&& t.AccountId == initialModel.SumIn.Account.Id
						&& t.CategoryId == initialModel.Category.Id
					)));
		}

		[TestMethod]
		public void EditPost_ShouldReadPreviousVersionOfTag_WhenCorrectTransactionPosted()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleEditModel();

			controller.Edit(initialModel);

			_transactionTagRepositoryMock.Verify(o => o.GetByTransaction(It.Is<int>(t => t == initialModel.Id)), Times.Once());
		}


		[TestMethod]
		public void EditPost_ShouldDeleteUnusedTag_WhenNewTagsReplacesOldTags()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleEditModel();
			initialModel.Tags = "Tag 3, Tag 4";

			controller.Edit(initialModel);

			_transactionTagRepositoryMock.Verify(o => o.Delete(
				It.Is<TransactionTag>(t => t.Id == TransactionTags.First().Id)), Times.Once());

			_transactionTagRepositoryMock.Verify(o => o.Delete(
				It.Is<TransactionTag>(t => t.Id == TransactionTags.Skip(1).First().Id)), Times.Once());
		}


		[TestMethod]
		public void EditPost_ShouldInsertNewTag_WhenOldTagsAreDeleted()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleEditModel();
			initialModel.Tags = "Tag 3, Tag 4";

			controller.Edit(initialModel);

			_transactionTagRepositoryMock.Verify(o => o.Insert(
				It.Is<TransactionTag>(
					t => t.Id == 0
						&& t.SectionId == SectionId
						&& t.Tag == "Tag 3"
						&& t.TransactionId == initialModel.Id
						&& t.CreatedBy == _currentUser.Id
						&& t.CreatedWhen >= DateTime.UtcNow.Date
					)), Times.Once());

			_transactionTagRepositoryMock.Verify(o => o.Insert(
				It.Is<TransactionTag>(
					t => t.Id == 0
						&& t.SectionId == SectionId
						&& t.Tag == "Tag 4"
						&& t.TransactionId == initialModel.Id
						&& t.CreatedBy == _currentUser.Id
						&& t.CreatedWhen >= DateTime.UtcNow.Date
					)), Times.Once());
		}

		[TestMethod]
		public void EditPost_ShouldInsertNewTag_WhenNewTagsAreAdded()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleEditModel();
			initialModel.Tags += ", Tag 3, Tag 4";

			controller.Edit(initialModel);

			_transactionTagRepositoryMock.Verify(o => o.Insert(
				It.Is<TransactionTag>(
					t => t.Id == 0
						&& t.SectionId == SectionId
						&& t.Tag == "Tag 3"
						&& t.TransactionId == initialModel.Id
						&& t.CreatedBy == _currentUser.Id
						&& t.CreatedWhen >= DateTime.UtcNow.Date
					)));

			_transactionTagRepositoryMock.Verify(o => o.Insert(
				It.Is<TransactionTag>(
					t => t.Id == 0
						&& t.SectionId == SectionId
						&& t.Tag == "Tag 4"
						&& t.TransactionId == initialModel.Id
						&& t.CreatedBy == _currentUser.Id
						&& t.CreatedWhen >= DateTime.UtcNow.Date
					)));
		}

		[TestMethod]
		public void EditPost_ShouldNotDeleteOldTag_WhenNewTagsAreAdded()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleEditModel();
			initialModel.Tags += ", Tag 3, Tag 4";

			controller.Edit(initialModel);

			_transactionTagRepositoryMock.Verify(o => o.Delete(
				It.Is<TransactionTag>(t => t.Id == TransactionTags.First().Id)), Times.Never());

			_transactionTagRepositoryMock.Verify(o => o.Delete(
				It.Is<TransactionTag>(t => t.Id == TransactionTags.Skip(1).First().Id)), Times.Never());
		}

		[TestMethod]
		public void EditPost_ShouldNotDeleteOldTag_WhenNewTagsAreAddedAtBeginning()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleEditModel();
			initialModel.Tags = "Tag 3, Tag 4, " + initialModel.Tags;

			controller.Edit(initialModel);

			_transactionTagRepositoryMock.Verify(o => o.Delete(
				It.Is<TransactionTag>(t => t.Id == TransactionTags.First().Id)), Times.Never());

			_transactionTagRepositoryMock.Verify(o => o.Delete(
				It.Is<TransactionTag>(t => t.Id == TransactionTags.Skip(1).First().Id)), Times.Never());
		}


		[TestMethod]
		public void EditPost_ShouldInsertLinkedTransactionRepository_WhenChangedFromNonLinkedToLinked()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleEditModel(TransactionTypes.Transfer);

			var result = controller.Edit(initialModel);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

			_transactionRepositoryMock.Verify(
				o => o.Update(It.Is<Transaction>(
					t => t.Amount == initialModel.Amount
						&& t.IsDisabled == initialModel.IsDisabled
						&& t.Formula == initialModel.SumOut.Formula
						&& t.Sum == -initialModel.SumOut.Sum
						&& t.Id == initialModel.Id
						&& t.Tags == initialModel.Tags
						&& t.LinkedId == null
						&& t.CurrencyId == initialModel.SumOut.Currency.Id
						&& t.AccountId == initialModel.SumOut.Account.Id
						&& t.CategoryId == initialModel.Category.Id
					)));

			_transactionRepositoryMock.Verify(
				o => o.Insert(It.Is<Transaction>(
					t => t.Amount == initialModel.Amount
						&& t.IsDisabled == initialModel.IsDisabled
						&& t.Formula == initialModel.SumIn.Formula
						&& t.Sum == initialModel.SumIn.Sum
						&& t.Tags == initialModel.Tags
						&& t.LinkedId == initialModel.Id
						&& t.CurrencyId == initialModel.SumIn.Currency.Id
						&& t.AccountId == initialModel.SumIn.Account.Id
						&& t.CategoryId == initialModel.Category.Id
					)));

			_transactionLinkRepositoryMock.Verify(
				o => o.Insert(It.Is<TransactionLink>(
					t => t.ParentId == initialModel.Id
						&& t.ChildId > 0
					)));
		}

		[TestMethod]
		public void EditPost_ShouldDeleteFromLinkedTransactionRepository_WhenChangedFromLinkedToNonLinked()
		{
			var tr1 = GetSimpleTransaction(TransactionTypes.Expense);
			var tr2 = GetSimpleTransaction(TransactionTypes.Income);
			tr2.Id = 2;

			var controller = GetTransactionController(tr1, tr2);

			var initialModel = GetSimpleEditModel(TransactionTypes.Expense);

			var result = controller.Edit(initialModel);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

			_transactionRepositoryMock.Verify(
				o => o.Update(It.Is<Transaction>(
					t => t.Amount == initialModel.Amount
						&& t.IsDisabled == initialModel.IsDisabled
						&& t.Formula == initialModel.SumOut.Formula
						&& t.Sum == -initialModel.SumOut.Sum
						&& t.Id == tr1.Id
						&& t.Tags == initialModel.Tags
						&& t.LinkedId == null
						&& t.CurrencyId == initialModel.SumOut.Currency.Id
						&& t.AccountId == initialModel.SumOut.Account.Id
						&& t.CategoryId == initialModel.Category.Id
					)));

			_transactionRepositoryMock.Verify(
				o => o.Delete(It.Is<int>(t => t == 2)));

			_transactionLinkRepositoryMock.Verify(
				o => o.Delete(It.Is<int>(t => t > 0)));
		}

		#endregion


		#region Create POST

		[TestMethod]
		public void CreatePost_ShouldRedirectToList_WhenCorrectTransactionPosted()
		{
			var controller = GetTransactionController();

			var model1 = GetSimpleCreateModel();

			var result = controller.Create(model1);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

			var redirectResult = (RedirectToRouteResult)result;

			Assert.AreEqual("list", redirectResult.RouteValues["action"], "Should redirect to list");
		}

		[TestMethod]
		public void CreatePost_ShouldInsertToTransactionRepository_WhenCorrectTransactionPosted()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleCreateModel();

			var result = controller.Create(initialModel);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

			_transactionRepositoryMock.Verify(
				o => o.Insert(It.Is<Transaction>(
					t => t.Amount == initialModel.Amount
						&& t.IsDisabled == initialModel.IsDisabled
						&& t.Formula == initialModel.SumIn.Formula
						&& t.Sum == initialModel.SumIn.Sum
						&& t.Tags == initialModel.Tags
						&& t.LinkedId == initialModel.LinkedId
						&& t.CurrencyId == initialModel.SumIn.Currency.Id
						&& t.AccountId == initialModel.SumIn.Account.Id
						&& t.CategoryId == initialModel.Category.Id
					)));
		}

		[TestMethod]
		public void CreatePost_ShouldNotInsertTagRepository_WhenCreatedTransactionWithoutTag()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleCreateModel();
			initialModel.Tags = null;

			controller.Create(initialModel);

			_transactionTagRepositoryMock.Verify(o => o.Insert(It.IsAny<TransactionTag>()), Times.Never());
		}

		[TestMethod]
		public void CreatePost_ShouldInsertTagRepository_WhenCreatedTransactionWithOneTag()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleCreateModel();
			initialModel.Tags = "Tag 7";
			initialModel.Id = 99;

			controller.Create(initialModel);

			_transactionTagRepositoryMock.Verify(
				o => o.Insert(It.Is<TransactionTag>(
					t => t.SectionId == SectionId
						&& t.IsDisabled == false
						&& t.Tag == "Tag 7"
						&& t.TransactionId == initialModel.Id
						&& t.Id == 0
					)), Times.Once());
		}

		[TestMethod]
		public void CreatePost_ShouldInsertTagRepository_WhenCreatedTransactionWithTwoTags()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleCreateModel();
			initialModel.Id = 99;

			controller.Create(initialModel);

			_transactionTagRepositoryMock.Verify(
				o => o.Insert(It.Is<TransactionTag>(
					t => t.SectionId == SectionId
						&& t.IsDisabled == false
						&& t.Tag == _defailtTags[0]
						&& t.CreatedWhen >= DateTime.UtcNow.Date
						&& t.Id == 0
						&& t.TransactionId == initialModel.Id
					)), Times.Once());

			_transactionTagRepositoryMock.Verify(
				o => o.Insert(It.Is<TransactionTag>(
					t => t.SectionId == SectionId
						&& t.IsDisabled == false
						&& t.Tag == _defailtTags[1]
						&& t.CreatedWhen >= DateTime.UtcNow.Date
						&& t.Id == 0
						&& t.TransactionId == initialModel.Id
					)), Times.Once());
		}

		[TestMethod]
		public void CreatePost_ShouldInsertTransactionLinkRepository_WhenCreatedTransferTransaction()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleCreateModel();
			initialModel.TransactionType.Id = (int)TransactionTypes.Transfer;
			initialModel.LinkedId = 1;
			initialModel.SumOut = new CurrencyBundle
				{
					Account = new IdWithSelectList { Id = DefaultAccountIdOut },
					Currency = new IdWithSelectList { Id = DefaultCurrencyId },
					Formula = "100+1",
					Sum = 101m
				};
			initialModel.SumIn = new CurrencyBundle
				{
					Account = new IdWithSelectList { Id = DefaultAccountIdIn },
					Currency = new IdWithSelectList { Id = DefaultCurrencyId },
					Formula = "100+1",
					Sum = 101m
				};

			controller.Create(initialModel);

			_transactionLinkRepositoryMock.Verify(
				o => o.Insert(It.Is<TransactionLink>(
					t => t.ParentId == _transacion.Id
						&& t.ChildId == _transacion.Id
						&& t.CreatedBy == _currentUser.Id
						&& t.CreatedWhen >= DateTime.UtcNow.Date
					)), Times.Once());
		}

		[TestMethod]
		public void CreatePost_ShouldNotInsertTransactionLinkRepository_WhenCreatedSimpleTransaction()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleCreateModel();
			initialModel.TransactionType.Id = (int)TransactionTypes.Expense;
			initialModel.LinkedId = 1;
			initialModel.SumOut = new CurrencyBundle
			{
				Account = new IdWithSelectList { Id = DefaultAccountIdOut },
				Currency = new IdWithSelectList { Id = DefaultCurrencyId },
				Formula = "100+1",
				Sum = 101m
			};

			controller.Create(initialModel);

			_transactionLinkRepositoryMock.Verify(
				o => o.Insert(It.IsAny<TransactionLink>()), Times.Never());
		}

		[TestMethod]
		public void CreatePost_ShouldChangeSumToNegative_WhenOutcomeTransactionAndPositiveSum()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleCreateModel();
			initialModel.TransactionType.Id = (int)TransactionTypes.Expense;
			initialModel.SumOut.Sum = 100m;

			controller.Create(initialModel);

			_transactionRepositoryMock.Verify(
				o => o.Insert(It.Is<Transaction>(
					t => t.TransactionTypeId == (int)TransactionTypes.Expense
						&& t.Sum == -100m
					)), Times.Once());
		}

		[TestMethod]
		public void CreatePost_ShouldChangeSumToNegative_WhenOutcomeTransactionAndNegativeSum()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleCreateModel();
			initialModel.TransactionType.Id = (int)TransactionTypes.Expense;
			initialModel.SumOut.Sum = -100m;

			controller.Create(initialModel);

			_transactionRepositoryMock.Verify(
				o => o.Insert(It.Is<Transaction>(
					t => t.TransactionTypeId == (int)TransactionTypes.Expense
						&& t.Sum == -100m
					)), Times.Once());
		}

		[TestMethod]
		public void CreatePost_ShouldChangeSumToNegative_WhenIncomeTransactionAndNegativeSum()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleCreateModel();
			initialModel.TransactionType.Id = (int)TransactionTypes.Income;
			initialModel.SumIn.Sum = -100m;

			controller.Create(initialModel);

			_transactionRepositoryMock.Verify(
				o => o.Insert(It.Is<Transaction>(
					t => t.TransactionTypeId == (int)TransactionTypes.Income
						&& t.Sum == 100m
					)), Times.Once());
		}

		[TestMethod]
		public void CreatePost_ShouldChangeSumToNegative_WhenIncomeTransactionAndPositiveSum()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleCreateModel();
			initialModel.TransactionType.Id = (int)TransactionTypes.Income;
			initialModel.SumIn.Sum = 100m;

			controller.Create(initialModel);

			_transactionRepositoryMock.Verify(
				o => o.Insert(It.Is<Transaction>(
					t => t.TransactionTypeId == (int)TransactionTypes.Income
						&& t.Sum == 100m
					)), Times.Once());
		}

		#endregion


		#region Create GET

		[TestMethod]
		public void CreateView_ShouldNotPassValidation_WhenModelInvalid()
		{
			var controller = GetTransactionController();

			var initialModel = GetSimpleCreateModel();
			initialModel.Date = DateTime.MinValue;
			controller.ModelState.AddModelError("Date", "Date is empty");

			var result = controller.Create(initialModel);

			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(TransactionEditViewModel));

			var model = ((ViewResult)result).Model as TransactionEditViewModel;

			AssertAfterCreateFailed(initialModel, ((ViewResult)result).Model as TransactionEditViewModel);
		}

		#endregion


		#region Helpers

		private TransactionsController GetTransactionController(Transaction defaultTransaction1 = null, Transaction defaultTransaction2 = null)
		{
			SetupTransactionRepository(defaultTransaction1, defaultTransaction2);
			SetupTransactionDataHelperMock();
			SetupTransactionLinkRepository(defaultTransaction1, defaultTransaction2);
			SetupDictionariesMock();
			SetupMembershipHelperMock();

			var controller = new TransactionsController
								{
									MembershipHelper = _membershipHelper.Object,
									TransactionRepository = _transactionRepositoryMock.Object,
									CurrencyRepository = _currencyRepositoryMock.Object,
									AccountRepository = _accountRepositoryMock.Object,
									TransactionTagRepository = _transactionTagRepositoryMock.Object,
									TransactionLinkRepository = _transactionLinkRepositoryMock.Object,
									CategoryRepository = _categoryRepositoryMock.Object,
									TransactionDataHelper = _transactionDataHelperMock.Object,
									Dictionaries = _dictionariesMock.Object,
                                    LogWriter = LogWriterMockHelper.CreateMock().Object
								};
			return controller;
		}

		private void SetupTransactionRepository(Transaction defaultTransaction1 = null, Transaction defaultTransaction2 = null)
		{
			_transactionRepositoryMock
				.Setup(o => o.GetById(It.Is<int>(p => p == (defaultTransaction1 ?? _transacion).Id)))
				.Returns(defaultTransaction1 ?? _transacion);

			_transactionRepositoryMock
				.Setup(o => o.Insert(It.IsAny<Transaction>()))
				.Returns<Transaction>(t =>
				{
					var t2 = new Transaction { Id = (defaultTransaction1 ?? _transacion).Id };
					return t2;
				});

			if (defaultTransaction2 != null)
			{
				var linked = new LinkedTransactions
				{
					First = defaultTransaction1 ?? _transacion,
					Second = defaultTransaction2
				};

				_transactionRepositoryMock
					.Setup(o => o.GetLinked(It.Is<int>(p => p == linked.First.Id || p == linked.Second.Id)))
					.Returns(linked);
			}
			else
			{
				var linked = new LinkedTransactions
				{
					First = defaultTransaction1 ?? _transacion,
					Second = null
				};

				_transactionRepositoryMock
					.Setup(o => o.GetLinked(It.Is<int>(p => p == (defaultTransaction1 ?? _transacion).Id)))
					.Returns(linked);
			}
		}

		private void SetupTransactionLinkRepository(Transaction defaultTransaction1 = null, Transaction defaultTransaction2 = null)
		{
			if (defaultTransaction2 == null)
				_transactionLinkRepositoryMock
					.Setup(o => o.GetByTransactionId(It.Is<int>(p => p > 0)))
					.Returns((TransactionLink)null);
			else
			{
				var linked = new TransactionLink
								{
									Id = 9,
									ParentId = (defaultTransaction1 ?? _transacion).Id,
									ChildId = defaultTransaction2.Id
								};

				_transactionLinkRepositoryMock
					.Setup(o => o.GetByTransactionId(It.Is<int>(p => p == linked.ParentId || p == linked.ChildId)))
					.Returns(linked);
			}
		}

		private void SetupTransactionDataHelperMock()
		{
			_transactionDataHelperMock
				.Setup(o => o.GetRealSumValue(
					It.Is<TransactionTypes>(t => new[] { TransactionTypes.Income, TransactionTypes.Exchange, TransactionTypes.Transfer, }.Contains(t)),
					It.IsAny<decimal>()))
				.Returns<TransactionTypes, decimal>((type, sum) => Math.Abs(sum));
            _transactionDataHelperMock
                .Setup(o => o.GetRealSumValue(
                    It.Is<int>(t => new[] { (int)TransactionTypes.Income, (int)TransactionTypes.Exchange, (int)TransactionTypes.Transfer, }.Contains(t)),
                    It.IsAny<decimal>()))
                .Returns<TransactionTypes, decimal>((type, sum) => Math.Abs(sum));

			_transactionDataHelperMock
				.Setup(o => o.GetRealSumValue(
					It.Is<TransactionTypes>(t => new[] { TransactionTypes.Expense, }.Contains(t)),
					It.IsAny<decimal>()))
				.Returns<TransactionTypes, decimal>((type, sum) => -Math.Abs(sum));
            _transactionDataHelperMock
                .Setup(o => o.GetRealSumValue(
                    It.Is<int>(t => new[] { (int)TransactionTypes.Expense, }.Contains(t)),
                    It.IsAny<decimal>()))
                .Returns<TransactionTypes, decimal>((type, sum) => -Math.Abs(sum));

			_transactionDataHelperMock
				.Setup(o => o.NormalizeTags(It.IsAny<string>()))
				.Returns<string>((s) => s);

			_transactionDataHelperMock
				.Setup(o => o.ParseStringWithTags(It.IsAny<string>()))
				.Returns<string>((s) => (s ?? "").Split(',').Select(o => o.Trim()));

		}

		private void SetupMembershipHelperMock()
		{
			_membershipHelper.Setup(o => o.CurrentUser)
				.Returns(_currentUser);
			_membershipHelper.Setup(o => o.GetUser())
				.Returns(_currentUser);
		}

		private void SetupDictionariesMock()
		{
			_dictionariesMock
				.Setup(o => o.Categories())
				.Returns(new[]
				         	{
				         		new Category {Id = 1, SectionId = SectionId, Name = "Category 1"},
				         		new Category {Id = DefaultCategoryId, SectionId = SectionId, Name = "Category 2", IsDefault = true},
				         	});

			_dictionariesMock
				.Setup(o => o.Currencies())
				.Returns(new[]
				         	{
				         		new Currency { Id = 1, Name = "Currency 1" },
								new Currency { Id = DefaultCurrencyId, Name = "Currency 2", IsDefault = true},
				         	}.AsQueryable());


			_dictionariesMock
				.Setup(o => o.Accounts())
				.Returns(new[]
				         	{
				         		new Account { Id = 1, Name = "Account 1" },
								new Account { Id = DefaultAccountIdOut, Name = "Account 2", IsDefault = true},
				         	}.AsQueryable());
		}

		private TransactionEditViewModel GetSimpleCreateModel(TransactionTypes type = TransactionTypes.Income)
		{
			var model = new TransactionEditViewModel
			{
				Id = 0,
				LinkedId = _transacion.LinkedId,
				Amount = _transacion.Amount,
				Date = _transacion.Date,
				Description = _transacion.Description,
				Category = new IdWithSelectList { Id = _transacion.CategoryId ?? 0 },
				TransactionType = new IdWithSelectList { Id = (int)type },
				Tags = _transacion.Tags,
				IsDisabled = _transacion.IsDisabled,
			};

			switch (type)
			{
				case TransactionTypes.Income:
					model.SumIn = new CurrencyBundle
									{
										Formula = _transacion.Formula,
										Sum = _transacion.Sum,
										Account = new IdWithSelectList { Id = _transacion.AccountId },
										Currency = new IdWithSelectList { Id = _transacion.CurrencyId },
									};
					break;
				case TransactionTypes.Expense:
					model.SumOut = new CurrencyBundle
									{
										Formula = _transacion.Formula,
										Sum = _transacion.Sum,
										Account = new IdWithSelectList { Id = _transacion.AccountId },
										Currency = new IdWithSelectList { Id = _transacion.CurrencyId },
									};
					break;
				case TransactionTypes.Transfer:
					model.SumOut = new CurrencyBundle
									{
										Formula = _transacion.Formula,
										Sum = _transacion.Sum,
										Account = new IdWithSelectList { Id = _transacion.AccountId },
										Currency = new IdWithSelectList { Id = _transacion.CurrencyId },
									};
					model.SumIn = new CurrencyBundle
									{
										Formula = _transacion.Formula,
										Sum = _transacion.Sum,
										Account = new IdWithSelectList { Id = _transacion.AccountId },
										Currency = new IdWithSelectList { Id = _transacion.CurrencyId },
									};
					break;
				case TransactionTypes.Exchange:
					model.SumOut = new CurrencyBundle
									{
										Formula = _transacion.Formula,
										Sum = _transacion.Sum,
										Account = new IdWithSelectList { Id = _transacion.AccountId },
										Currency = new IdWithSelectList { Id = _transacion.CurrencyId },
									};
					model.SumIn = new CurrencyBundle
									{
										Formula = _transacion.Formula,
										Sum = _transacion.Sum,
										Account = new IdWithSelectList { Id = _transacion.AccountId },
										Currency = new IdWithSelectList { Id = _transacion.CurrencyId },
									};
					break;
				default:
					break;
			}

			return model;
		}

		private TransactionEditViewModel GetSimpleEditModel(TransactionTypes type = TransactionTypes.Income)
		{
			var model = GetSimpleCreateModel(type);
			model.Id = _transacion.Id;
			return model;
		}

		private Transaction GetSimpleTransaction(TransactionTypes type = TransactionTypes.Income)
		{
			return new Transaction
			{
				Id = 12345,
				AccountId = DefaultAccountIdOut,
				CurrencyId = DefaultCurrencyId,
				CategoryId = DefaultCategoryId,
				SectionId = SectionId,
				TransactionTypeId = (int)type,
				Amount = 1m,
				Sum = 123m * (type == TransactionTypes.Expense ? -1 : 1),
				Formula = "100+23",
				CreatedBy = _createdUser.Id,
				Date = DateTime.UtcNow.Date,
				IsDisabled = false,
				LinkedId = null,
				Description = "Description",
				Tags = string.Join(", ", _defailtTags),
				CreatedWhen = DateTime.UtcNow,
			};
		}

		private void AssertAfterCreateFailed(TransactionEditViewModel sourceModel, TransactionEditViewModel resultModel)
		{
			Assert.AreEqual(0, resultModel.Id, "Id should be zero");
			Assert.AreEqual(sourceModel.Amount, resultModel.Amount, "Amount should be equal");
			Assert.AreEqual(sourceModel.SumIn.Sum, resultModel.SumIn.Sum, "Sum should be equal");
			Assert.AreEqual(sourceModel.SumIn.Formula, resultModel.SumIn.Formula, "Formula should be equal");
			Assert.AreEqual(sourceModel.SumIn.Currency.Id, resultModel.SumIn.Currency.Id, "CurrencyId should be equal");
			Assert.AreEqual(sourceModel.SumIn.Account.Id, resultModel.SumIn.Account.Id, "AccountId should be equal");
			//Assert.AreEqual(sourceModel.Date, resultModel.Date, "Date should be equal");
			Assert.AreEqual(sourceModel.Tags, resultModel.Tags, "Tags should be equal");
			Assert.AreEqual(sourceModel.Description, resultModel.Description, "Description should be equal");
			Assert.AreEqual(sourceModel.LinkedId, resultModel.LinkedId, "LinkedId should be equal");
			Assert.AreEqual(sourceModel.IsDisabled, resultModel.IsDisabled, "IsDisabled should be equal");
			Assert.AreEqual(sourceModel.Category.Id, resultModel.Category.Id, "CategoryId should be equal");
		}

		private void AssertOnGetEdit(Transaction transacion, TransactionEditViewModel resultModel)
		{
			Assert.AreEqual(transacion.Amount, resultModel.Amount, "Amount should be equal");
			Assert.AreEqual(transacion.TransactionTypeId, resultModel.TransactionType.Id, "TransactionType should be equal");
			Assert.AreEqual(transacion.Id, resultModel.Id, "Id should be equal");
			Assert.AreEqual(transacion.Date, resultModel.Date, "Date should be equal");
			Assert.AreEqual(transacion.Tags, resultModel.Tags, "Tags should be equal");
			Assert.AreEqual(transacion.Description, resultModel.Description, "Description should be equal");
			Assert.AreEqual(transacion.LinkedId, resultModel.LinkedId, "LinkedId should be equal");
			Assert.AreEqual(transacion.IsDisabled, resultModel.IsDisabled, "IsDisabled should be equal");

			switch ((TransactionTypes)transacion.TransactionTypeId)
			{
				case TransactionTypes.Income:
					Assert.AreEqual(transacion.Sum, resultModel.SumIn.Sum, "Sum should be equal");
					Assert.AreEqual(transacion.Formula, resultModel.SumIn.Formula, "Formula should be equal");
					Assert.AreEqual(transacion.CurrencyId, resultModel.SumIn.Currency.Id, "CurrencyId should be equal");

					Assert.AreEqual(0m, resultModel.SumOut.Sum, "Sum should be empty");
					Assert.IsNull(resultModel.SumOut.Formula, "Formula should be empty");
					Assert.AreEqual(0, resultModel.SumOut.Currency.Id, "CurrencyId should be empty");
					break;
				case TransactionTypes.Expense:
					Assert.AreEqual(0m, resultModel.SumIn.Sum, "Sum should be empty");
					Assert.IsNull(resultModel.SumIn.Formula, "Formula should be empty");
					Assert.AreEqual(0, resultModel.SumIn.Currency.Id, "CurrencyId should be empty");

					Assert.AreEqual(Math.Abs(transacion.Sum), resultModel.SumOut.Sum, "Sum should be equal");
					Assert.AreEqual(transacion.Formula, resultModel.SumOut.Formula, "Formula should be equal");
					Assert.AreEqual(transacion.CurrencyId, resultModel.SumOut.Currency.Id, "CurrencyId should be equal");
					break;
				case TransactionTypes.Transfer:
					break;
				case TransactionTypes.Exchange:
					break;
				default:
					break;
			}
		}

		#endregion
	}
}
