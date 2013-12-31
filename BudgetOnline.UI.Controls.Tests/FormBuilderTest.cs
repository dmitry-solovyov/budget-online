using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.UI.Controls.Tests
{
	[TestClass]
	public class FormBuilderTest
	{
		[TestMethod]
		public void FormBuilderTest_Simle()
		{
			var builder = new FormBuilder();

			var s = builder.Build().ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<form method=""post""></form>", s);
		}

		[TestMethod]
		public void FormBuilderTest_WithCssClass()
		{
			var builder = new FormBuilder();

			var s = builder.Css("sign-in").Build().ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<form method=""post"" class=""sign-in""></form>", s);
		}

		[TestMethod]
		public void FormBuilderTest_WithTwoCssClasses()
		{
			var builder = new FormBuilder();

			var s = builder.Css("sign-in").Css("sign-out").Build().ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<form method=""post"" class=""sign-in sign-out""></form>", s);
		}

		[TestMethod]
		public void FormBuilderTest_WithContent()
		{
			var builder = new FormBuilder();

			var s = builder.Css("sign-in").Build().ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<form method=""post"" class=""sign-in""></form>", s);
		}

		[TestMethod]
		public void FormBuilderTest_WithHeader()
		{
			var builder = new FormBuilder();

			var s = builder.Css("sign-in").Header("Hello!").HeaderCss("form-signin-heading").Build().ToHtmlString();

			Console.WriteLine(s);

			Assert.AreEqual(@"<form method=""post"" class=""sign-in""><h2 class=""form-signin-heading"">Hello!</h2></form>", s);
		}
	}
}
