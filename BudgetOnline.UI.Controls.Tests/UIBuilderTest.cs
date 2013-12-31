using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.UI.Controls.Tests
{
	[TestClass]
	public class UIBuilderTest
	{
		[TestMethod]
		public void UIBuilderTest_CreateEmptyTag()
		{
			var builder = new UIBuilder();
			var s = builder.Tag("div").Build().ToHtmlString();

			Assert.AreEqual("<div />", s);
		}

		[TestMethod]
		public void UIBuilderTest_CreateWithId()
		{
			var builder = new UIBuilder();
			var s = builder.Tag("div").Id("#id0").Build().ToHtmlString();

			Assert.AreEqual(@"<div id=""#id0"" />", s);
		}

		[TestMethod]
		public void UIBuilderTest_CreateWithOverwritedTag()
		{
			var builder = new UIBuilder();
			var s = builder.Tag("div1").Tag("div2").Build().ToHtmlString();

			Assert.AreEqual(@"<div2 />", s);
		}

		[TestMethod]
		public void UIBuilderTest_CreateEmptyTagWithOneAttr()
		{
			var builder = new UIBuilder();
			var s = builder.Tag("div").Attr("attr", "value").Build().ToHtmlString();

			Assert.AreEqual(@"<div attr=""value"" />", s);
		}

		[TestMethod]
		public void UIBuilderTest_CreateEmptyTagWithTwoAttrs()
		{
			var builder = new UIBuilder();
			var s = builder.Tag("div").Attr("attr1", "value1").Attr("attr2", "value2").Build().ToHtmlString();

			Assert.AreEqual(@"<div attr1=""value1"" attr2=""value2"" />", s);
		}

		[TestMethod]
		public void UIBuilderTest_CreateEmptyTagWithTwoAttrs_EmptyFirstValue()
		{
			var builder = new UIBuilder();
			var s = builder.Tag("div").Attr("attr1", "").Attr("attr2", "value2").Build().ToHtmlString();

			Assert.AreEqual(@"<div attr2=""value2"" />", s);
		}

		[TestMethod]
		public void UIBuilderTest_CreateEmptyTagWithTwoAttrs_EmptySecondValue()
		{
			var builder = new UIBuilder();
			var s = builder.Tag("div").Attr("attr1", "value1").Attr("attr2", "").Build().ToHtmlString();

			Assert.AreEqual(@"<div attr1=""value1"" />", s);
		}

		[TestMethod]
		public void UIBuilderTest_TagWithDynamicContent()
		{
			var builder = new UIBuilder();
			var s = builder.Tag("div").Content("text").Build().ToHtmlString();

			Assert.AreEqual(@"<div>text</div>", s);
		}

		[TestMethod]
		public void UIBuilderTest_TagWithStaticContent()
		{
			var builder = new UIBuilder();
			var s = builder.Tag("div").Content("text").Build().ToHtmlString();

			Assert.AreEqual(@"<div>text</div>", s);
		}

		[TestMethod]
		public void UIBuilderTest_TagWithTextAndAttr()
		{
			var builder = new UIBuilder();
			var s = builder.Tag("div").Content("text").Attr("attr", "value").Build().ToHtmlString();

			Assert.AreEqual(@"<div attr=""value"">text</div>", s);
		}

		[TestMethod]
		public void UIBuilderTest_TagWithChild()
		{
			var builder = new UIBuilder();
			var s = builder.Tag("div")
				.Child(() => new UIBuilder().Tag("input"))
				.Build()
				.ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<div><input /></div>", s);
		}

		[TestMethod]
		public void UIBuilderTest_TagWithChild_ContentIgnored()
		{
			var builder = new UIBuilder();
			var s = builder.Tag("div")
				.Content("AAA")
				.Child(() => new UIBuilder().Tag("input"))
				.Build()
				.ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<div><input /></div>", s);
		}
		
		[TestMethod]
		public void UIBuilderTest_TagWithTwoChilds()
		{
			var builder = new UIBuilder();
			var s = builder
				.Tag("div")
				.Child(() => new UIBuilder().Tag("input1").Content("value1").Attr("attr1", "value1"))
				.Child(() => new UIBuilder().Tag("input2").Content("value2").Attr("attr2", "value2"))
				.Build()
				.ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<div><input1 attr1=""value1"">value1</input1><input2 attr2=""value2"">value2</input2></div>", s);
		}

		[TestMethod]
		public void UIBuilderTest_TagWithTwoNestedChilds()
		{
			var builder = new UIBuilder();
			var s = builder
				.Tag("div")
				.Child(() => 
					new UIBuilder().Tag("input1").Content("value1").Attr("attr1", "value1")
						.Child(() => 
							new UIBuilder().Tag("input2").Content("value2").Attr("attr2", "value2"))
				)				
				.Build()
				.ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<div><input1 attr1=""value1""><input2 attr2=""value2"">value2</input2></input1></div>", s);
		}
	}
}
