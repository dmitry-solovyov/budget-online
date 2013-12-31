using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.UI.Controls.Tests
{
	[TestClass]
	public class ImageBuilderTest
	{
		[TestMethod]
		public void ImageBuilderTest_Simple()
		{
			var builderResult = new ImageBuilder().Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<img />", builderResult);
		}
	
		[TestMethod]
		public void ImageBuilderTest_WithSrc()
		{
			var builderResult = new ImageBuilder().Src("path", "text").Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<img src=\"path\" title=\"text\" />", builderResult);
		}

		[TestMethod]
		public void ImageBuilderTest_WithClass()
		{
			var builderResult = new ImageBuilder().Css("icon").Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual(@"<img class=""icon"" />", builderResult);
		}

		[TestMethod]
		public void ImageBuilderTest_WithSrcAndClass()
		{
			var builderResult = new ImageBuilder().Css("icon").Src("path", "text").Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual(@"<img class=""icon"" src=""path"" title=""text"" />", builderResult);
		}


		[TestMethod]
		public void ImageBuilderTest_ImageInsideControl()
		{
			var builderResult = new UIBuilder()
				.Content(new ImageBuilder().Css("icon").Src("path", "text"))
				.Build().ToHtmlString();

			Console.WriteLine(builderResult);

			//Console.WriteLine(@"<button class=""class""><image class=""icon"" src=""path"" tooltip=""tip"" /><span class=""caption"">Caption</span></button>");
			//Console.WriteLine(s);

			Assert.AreEqual(@"<div><img class=""icon"" src=""path"" title=""text"" /></div>", builderResult);
		}
	}
}
