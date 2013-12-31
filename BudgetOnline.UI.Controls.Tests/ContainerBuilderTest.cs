using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.UI.Controls.Tests
{
	[TestClass]
	public class ContainerBuilderTest
	{
		[TestMethod]
		public void ContainerBuilderTest_CreateEmptyTag_WhenHideIfEmptyTurnedOn()
		{
			var builder = new ContainerBuilder().HideIfEmpty(true);
			var s = builder.Tag("div").Build().ToHtmlString();

			Assert.AreEqual(string.Empty, s);
		}

		[TestMethod]
		public void ContainerBuilderTest_CreateEmptyTag_WhenHideIfEmptyTurnedOff()
		{
			var builder = new ContainerBuilder();
			var s = builder.Tag("div").Build().ToHtmlString();

			Assert.AreEqual("<div />", s);
		}

		[TestMethod]
		public void ContainerBuilderTest_CreateContent()
		{
			var builder = new ContainerBuilder();
			var s = builder.Tag("div").Content("content").Build().ToHtmlString();

			Assert.AreEqual(@"<div>content</div>", s);
		}

		[TestMethod]
		public void ContainerBuilderTest_CreateWithOneChildElementInContent()
		{
			var builder = new ContainerBuilder();
			var s = builder.Tag("div1").Content(new UIBuilder().Tag("h1").Css("child").Content("HEADER")).Build().ToHtmlString();

			Assert.AreEqual(@"<div1><h1 class=""child"">HEADER</h1></div1>", s);
		}

	}
}
