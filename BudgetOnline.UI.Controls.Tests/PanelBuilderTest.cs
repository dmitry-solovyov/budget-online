using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.UI.Controls.Tests
{
	[TestClass]
	public class PanelBuilderTest
	{
		[TestMethod]
		public void SimlePanelBuilderTest_NotSuppressed()
		{
			var builder = new PanelBuilder()
				.SuppressFooterIfEmpty(false)
				.SuppressHeaderIfEmpty(false)
				.SuppressContentIfEmpty(false);

			var s = builder.Build().ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<div class=""bo-box""><div class=""header"" /><div class=""content"" /><div class=""footer"" /></div>", s);
		}

		[TestMethod]
		public void SimlePanelBuilderTest_HeaderSuppressed()
		{
			var builder = new PanelBuilder()
				.SuppressFooterIfEmpty(false)
				.SuppressHeaderIfEmpty(true)
				.SuppressContentIfEmpty(false);

			var s = builder.Build().ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<div class=""bo-box""><div class=""content"" /><div class=""footer"" /></div>", s);
		}

		[TestMethod]
		public void SimlePanelBuilderTest_FooterSuppressed()
		{
			var builder = new PanelBuilder()
				.SuppressFooterIfEmpty(true)
				.SuppressHeaderIfEmpty(false)
				.SuppressContentIfEmpty(false);

			var s = builder.Build().ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<div class=""bo-box""><div class=""header"" /><div class=""content"" /></div>", s);
		}

		[TestMethod]
		public void SimlePanelBuilderTest_ContentSuppressed()
		{
			var builder = new PanelBuilder()
				.SuppressFooterIfEmpty(false)
				.SuppressHeaderIfEmpty(false)
				.SuppressContentIfEmpty(true);

			var s = builder.Build().ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<div class=""bo-box""><div class=""header"" /><div class=""footer"" /></div>", s);
		}

		[TestMethod]
		public void PanelBuilderTest_HeaderAndContent()
		{
			var builder = new PanelBuilder();
			var s = builder
				.Header(new UIBuilder().Tag("div").Css("description1"))
				.Content(new UIBuilder().Tag("div").Css("description2"))
				.Build()
				.ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<div class=""bo-box""><div class=""header""><div class=""description1"" /></div><div class=""content""><div class=""description2"" /></div></div>", s);
		}

		[TestMethod]
		public void PanelBuilderTest_HeaderAndContentAndFooter()
		{
			var builder = new PanelBuilder();
			var s = builder
				.Header(() => new UIBuilder().Tag("div").Css("description1").Build())
				.Content(() => new UIBuilder().Tag("div").Css("description2").Build())
				.Footer(() => "Footer")
				.Build()
				.ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<div class=""bo-box""><div class=""header""><div class=""description1"" /></div><div class=""content""><div class=""description2"" /></div><div class=""footer"">Footer</div></div>", s);
		}
	}
}
