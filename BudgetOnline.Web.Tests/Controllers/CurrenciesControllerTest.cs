using System;
using System.Linq;
using System.Web.Mvc;
using BudgetOnline.Data.Manage.Contracts;
using BudgetOnline.Data.Manage.Types.Simple;
using BudgetOnline.Web.Areas.Admin.Controllers;
using BudgetOnline.Web.Areas.Admin.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BudgetOnline.Web.Tests.Controllers
{
	[TestClass]
	public class CurrenciesControllerTest : BaseControllerTest
	{
		private readonly Mock<ICurrencyRepository> _currencyRepositoryMock = new Mock<ICurrencyRepository>();

		private const int CurrencyId = 1;
		private Currency _currency;
		private Currency _currencyFromOtherSection;


		protected override void Setup()
		{
			_currency = new Currency
			{
				Id = 1,
				SectionId = 1,
				Name = "CURR",
				Symbol = "%",
				Description = "Description",
				CreatedWhen = DateTime.UtcNow,
				IsDisabled = true,
				IsDefault = true,
			};

			_currencyFromOtherSection = new Currency
			{
				Id = 2,
				SectionId = 2,
				Name = "XXX"
			};

			SetupCurrencyRepository();
		}

		//[TestMethod]
		public void ListView_ShouldReturnOnlyRowsInSection_WhenRequestedList()
		{

		}


		[TestMethod]
		public void EditView_ShouldReturnHttpNotFoundReposnse_WhenRowDoesntExist()
		{
			var controller = GetCurrencyController();

			var result = controller.Edit(-1);

			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}

		[TestMethod]
		public void EditView_ShouldReturnHttpNotFoundReposnse_WhenRowDoesntBelongsToSameSection()
		{
			var controller = GetCurrencyController();

			var result = controller.Edit(_currencyFromOtherSection.Id);

			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}


		[TestMethod]
		public void EditView_ShouldReturnView_WhenCorrectCurrencyRequested()
		{
			var controller = GetCurrencyController();

			var result = controller.Edit(_currency.Id);

			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(CurrencyEditViewModel));

			var model = ((ViewResult)result).Model as CurrencyEditViewModel;

			Assert.AreEqual(_currency.Symbol, model.Symbol, "Symbol should be equal");
			Assert.AreEqual(_currency.Name, model.Name, "Name should be equal");
			Assert.AreEqual(_currency.Id, model.Id, "Id should be equal");
			Assert.AreEqual(_currency.IsDefault, model.IsDefault, "IsDefault should be equal");
			Assert.AreEqual(_currency.IsDisabled, model.IsDisabled, "IsDisabled should be equal");
			Assert.AreEqual(_currency.Description, model.Description, "Description should be equal");
		}

		[TestMethod]
		public void EditView_ShouldSaveCurrencyToRepository_WhenCorrectCurrencyPosted()
		{
			var controller = GetCurrencyController();

			var model = new CurrencyEditViewModel { Id = 1, Name = "CURR", IsDefault = true, IsDisabled = true, Description = "DESC", Symbol = "@" };

			var result = controller.Edit(model);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
			Assert.IsTrue(((RedirectToRouteResult)result).RouteValues.Count > 0);

			var currency = new Currency
							{
								Id = model.Id,
								Name = model.Name,
								SectionId = CurrentUser.SectionId,
								Description = model.Description,
								IsDefault = model.IsDefault,
								IsDisabled = model.IsDisabled,
								Symbol = model.Symbol,
							};

			_currencyRepositoryMock.Verify(o => o.Update(It.IsAny<Currency>()), Times.Once(), "Should call Update method");
		}

		private CurrenciesController GetCurrencyController()
		{
			var controller = new CurrenciesController();
			controller.MembershipHelper = MembershipHelperMock.Object;
			controller.CacheWrapper = CacheWrapperMock.Object;
			controller.CurrencyRepository = _currencyRepositoryMock.Object;
			return controller;
		}


		private void SetupCurrencyRepository()
		{
			_currencyRepositoryMock
				.Setup(o => o.GetList(It.IsAny<int>()))
				.Returns(new[]
				         	{
				         		_currency,
								_currencyFromOtherSection,
				         	}.AsQueryable());

			_currencyRepositoryMock
				.Setup(o => o.Get(It.Is<int>(i => i == _currency.Id)))
				.Returns(_currency);

			_currencyRepositoryMock
				.Setup(o => o.Get(It.Is<int>(i => i != _currency.Id)))
				.Returns(_currencyFromOtherSection);
		}
	}
}
