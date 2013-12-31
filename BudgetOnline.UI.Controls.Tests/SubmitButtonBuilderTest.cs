using System;
using BudgetOnline.UI.Controls.Buttons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.UI.Controls.Tests
{
	[TestClass]
	public class SubmitButtonBuilderTest
	{
		[TestMethod]
		public void SubmitButtonBuilderTest_Simple()
		{
			var builderResult = new SubmitButtonBuilder().Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<button type=\"submit\"></button>", builderResult);
		}
	
		[TestMethod]
		public void SubmitButtonBuilderTest_WithCaption()
		{
			var builderResult = new SubmitButtonBuilder().Caption("Caption").Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<button type=\"submit\">Caption</button>", builderResult);
		}
	}
}
