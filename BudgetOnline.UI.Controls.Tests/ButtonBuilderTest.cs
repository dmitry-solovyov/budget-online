using System;
using BudgetOnline.UI.Controls.Buttons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.UI.Controls.Tests
{
	[TestClass]
	public class ButtonBuilderTest
	{
		[TestMethod]
		public void ButtonBuilderTest_Simple()
		{
			var builderResult = new ButtonBuilder().Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<button></button>", builderResult);
		}
	
		[TestMethod]
		public void ButtonBuilderTest_WithCaption()
		{
			var builderResult = new ButtonBuilder().Caption("Caption").Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<button>Caption</button>", builderResult);
		}

		[TestMethod]
		public void ButtonBuilderTest_WithClass()
		{
			var builderResult = new ButtonBuilder().Caption("Caption").Css("class").Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual(@"<button class=""class"">Caption</button>", builderResult);
		}


		[TestMethod]
		public void ButtonBuilderTest_WithIcon()
		{
			var builderResult = new ButtonBuilder().Caption("Caption").Css("btn").Icon("path", "tip").Build().ToHtmlString();
			Console.WriteLine(builderResult);

			Assert.AreEqual(@"<button class=""btn""><img class=""icon"" src=""path"" title=""tip"" /><span class=""caption"">Caption</span></button>", builderResult);
		}


		[TestMethod]
		public void ButtonBuilderTest_WithId()
		{
			var builderResult = new ButtonBuilder().Caption("Caption").Id("my-id").Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<button id=\"my-id\">Caption</button>", builderResult);
		}

		[TestMethod]
		public void ButtonBuilderTest_WithAttributes()
		{
			var builderResult = new ButtonBuilder().Caption("Caption").Id("my-id").Attr("attr1", "attr-value1").Attr("attr2", "attr-value2").Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<button id=\"my-id\" attr1=\"attr-value1\" attr2=\"attr-value2\">Caption</button>", builderResult);
		}
	}
}
