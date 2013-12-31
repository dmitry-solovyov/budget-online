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
	public class TagsControllerTest : BaseControllerTest
	{
		private readonly Mock<ITagRepository> _tagRepositoryMock = new Mock<ITagRepository>();

		private const int TagId = 1;
		private Tag _tag;
		private Tag _tagFromOtherSection;


		protected override void Setup()
		{
			_tag = new Tag
			{
				Id = 1,
				SectionId = 1,
				Name = "Tag1",
				CreatedWhen = DateTime.UtcNow,
				IsDisabled = true,
			};

			_tagFromOtherSection = new Tag
			{
				Id = 2,
				SectionId = 2,
				Name = "XXX"
			};

			SetupTagRepository();
		}

		//[TestMethod]
		public void ListView_ShouldReturnOnlyRowsInSection_WhenRequestedList()
		{

		}


		[TestMethod]
		public void EditView_ShouldReturnHttpNotFoundReposnse_WhenRowDoesntExist()
		{
			var controller = GetTagController();

			var result = controller.Edit(-1);

			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}

		[TestMethod]
		public void EditView_ShouldReturnHttpNotFoundReposnse_WhenRowDoesntBelongsToSameSection()
		{
			var controller = GetTagController();

			var result = controller.Edit(_tagFromOtherSection.Id);

			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}


		[TestMethod]
		public void EditView_ShouldReturnView_WhenCorrectTagRequested()
		{
			var controller = GetTagController();

			var result = controller.Edit(_tag.Id);

			Assert.IsInstanceOfType(result, typeof(ViewResult));
			Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(TagEditViewModel));

			var model = ((ViewResult)result).Model as TagEditViewModel;

			Assert.AreEqual(_tag.Name, model.Name, "Name should be equal");
			Assert.AreEqual(_tag.Id, model.Id, "Id should be equal");
			Assert.AreEqual(_tag.IsDisabled, model.IsDisabled, "IsDisabled should be equal");
			Assert.AreEqual(_tag.Hits, model.Hits, "Hits should be equal");
		}

		[TestMethod]
		public void EditView_ShouldSaveTagToRepository_WhenCorrectTagPosted()
		{
			var controller = GetTagController();

			var model = new TagEditViewModel { Id = 1, Name = "Tag", IsDisabled = true, Hits = 9 };

			var result = controller.Edit(model);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
			Assert.IsTrue(((RedirectToRouteResult)result).RouteValues.Count > 0);

			var tag = new Tag
							{
								Id = model.Id,
								Name = model.Name,
								SectionId = CurrentUser.SectionId,
								IsDisabled = model.IsDisabled,
								Hits = model.Hits,
							};

			_tagRepositoryMock.Verify(o => o.Update(It.IsAny<Tag>()), Times.Once(), "Should call Update method");
		}

		private TagsController GetTagController()
		{
			var controller = new TagsController();
			controller.MembershipHelper = MembershipHelperMock.Object;
			controller.CacheWrapper = CacheWrapperMock.Object;
			controller.TagRepository = _tagRepositoryMock.Object;
			return controller;
		}


		private void SetupTagRepository()
		{
			_tagRepositoryMock
				.Setup(o => o.GetList(It.IsAny<int>()))
				.Returns(new[]
				         	{
				         		_tag,
								_tagFromOtherSection,
				         	}.AsQueryable());

			_tagRepositoryMock
				.Setup(o => o.Get(It.Is<int>(i => i == _tag.Id)))
				.Returns(_tag);

			_tagRepositoryMock
				.Setup(o => o.Get(It.Is<int>(i => i != _tag.Id)))
				.Returns(_tagFromOtherSection);
		}
	}
}
