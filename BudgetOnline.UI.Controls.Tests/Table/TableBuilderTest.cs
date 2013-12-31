using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.UI.Controls.Tests.Table
{
	[TestClass]
	public class TableBuilderTest
	{
		class Model
		{
			[DisplayName("Col1")]
			public string F1 { get; set; }
			[DisplayName("Col2")]
			public int F2 { get; set; }
		}

		[TestMethod]
		public void TableBuilderTest_SimpleBuild()
		{
			var rows = new[]
			           	{
							new Model { F1 = "A1", F2 = 99 },
							new Model { F1 = "A2", F2 = 98 },
			           	};

			var builderResult = new TableBuilder<Model>()
				.Rows(rows)
				.Columns(columns =>
				         	{
				         		columns.Bound(m => m.F1);
								columns.Bound(m => m.F2);
				         	})
				.Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<table><thead><tr><th>Col1</th><th>Col2</th></tr></thead><tbody><tr><td>A1</td><td>99</td></tr><tr><td>A2</td><td>98</td></tr></tbody></table>", builderResult);
		}

		[TestMethod]
		public void TableBuilderTest_BuildWithoutRows()
		{
			var rows = new Model[0];

			var builderResult = new TableBuilder<Model>()
				.Rows(rows)
				.Columns(columns =>
				{
					columns.Bound(m => m.F1);
					columns.Bound(m => m.F2);
				})
				.Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<table><thead><tr><th>Col1</th><th>Col2</th></tr></thead><tbody></tbody></table>", builderResult);
		}

		[TestMethod]
		public void TableBuilderTest_BuildWithTableCss()
		{
			var rows = new Model[0];

			var builderResult = new TableBuilder<Model>()
				.Css("style")
				.Rows(rows)
				.Columns(columns =>
				{
					columns.Bound(m => m.F1);
					columns.Bound(m => m.F2);
				})
				.Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<table class=\"style\"><thead><tr><th>Col1</th><th>Col2</th></tr></thead><tbody></tbody></table>", builderResult);
		}

		[TestMethod]
		public void TableBuilderTest_WithRowStyle()
		{
			var rows = new[]
			           	{
							new Model { F1 = "red", F2 = 99 },
							new Model { F1 = "green", F2 = 98 },
			           	};

			var builderResult = new TableBuilder<Model>()
				.Rows(rows)
				.Columns(columns =>
				{
					columns.Bound(m => m.F1);
					columns.Bound(m => m.F2);
				})
				.RowStyleRetriever(m => m.F1 == "red" ? "red" : (m.F1 == "green" ? "green" : string.Empty))
				.Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<table><thead><tr><th>Col1</th><th>Col2</th></tr></thead><tbody><tr class=\"red\"><td>red</td><td>99</td></tr><tr class=\"green\"><td>green</td><td>98</td></tr></tbody></table>", builderResult);
		}


		[TestMethod]
		public void TableBuilderTest_BuildWithoutHeader()
		{
			var rows = new Model[0];

			var builderResult = new TableBuilder<Model>()
				.Css("style")
				.Rows(rows)
				.SupressHeader()
				.Columns(columns =>
				{
					columns.Bound(m => m.F1);
					columns.Bound(m => m.F2);
				})
				.Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<table class=\"style\"><tbody></tbody></table>", builderResult);
		}

		[TestMethod]
		public void TableBuilderTest_BuildIm()
		{
			var rows = new[]
			           	{
							new Model { F1 = "red", F2 = 99 },
			           	};

			var builderResult = new TableBuilder<Model>()
				.Css("style")
				.Rows(rows)
				.Columns(columns =>
				{
					columns.Icon().IconCss(m => m.F2 > 0 ? "plus" : "neg");
					columns.Bound(m => m.F2);
				})
				.Build().ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<table class=\"style\"><thead><tr><th></th><th>Col2</th></tr></thead><tbody><tr><td><i class=\"plus\"></i></td><td>99</td></tr></tbody></table>", builderResult);
		}

	}
}
