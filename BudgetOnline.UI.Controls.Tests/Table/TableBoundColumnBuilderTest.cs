using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.UI.Controls.Tests.Table
{
	[TestClass]
	public class TableBoundColumnBuilderTest
	{
	    readonly TableDefinitions _tableDefinition = new TableDefinitions();
		class Model
		{
			[DisplayName("Ff 11")]
			public string F1 { get; set; }
			[DisplayName("Ff 22")]
			public int F2 { get; set; }
			[DisplayName("F 3")]
			public DateTime F3 { get; set; }
		}

		[TestMethod]
		public void TableColumnBuilderTest_BuildHeader()
		{
			var model = new Model { F1 = "AA", F2 = 12345 };
			var builderResult = new TableBoundColumnBuilder<Model>().Binding(m => m.F1)
                .Build(_tableDefinition, ColumnRenderParts.Header).ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<th>Ff 11</th>", builderResult);
		}

		[TestMethod]
		public void TableColumnBuilderTest_BuildHeaderWithClass()
		{
			var model = new Model();
			var builderResult = new TableBoundColumnBuilder<Model>().Binding(m => m.F1)
				.HeaderCss("css")
                .Build(_tableDefinition, ColumnRenderParts.Header).ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual(@"<th class=""css"">Ff 11</th>", builderResult);
		}

		[TestMethod]
		public void TableColumnBuilderTest_BuildCell()
		{
			var model = new Model();
			var builderResult = new TableBoundColumnBuilder<Model>().Binding(m => m.F1)
                .Build(_tableDefinition, ColumnRenderParts.Cell, model).ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<td></td>", builderResult);
		}

		[TestMethod]
		public void TableColumnBuilderTest_BuildCellWithHeader()
		{
			var model = new Model();
			var builderResult = new TableBoundColumnBuilder<Model>().Binding(m => m.F1)
				.CellCss("css")
                .Build(_tableDefinition, ColumnRenderParts.Cell, model).ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<td class=\"css\"></td>", builderResult);
		}

		[TestMethod]
		public void TableColumnBuilderTest_BuildCellWithFormatterDateTime()
		{
			var dt = DateTime.Now;
			var model = new Model { F3 = dt };
			var builderResult = new TableBoundColumnBuilder<Model>().Binding(m => m.F3)
				.Formatter<DateTime>(o => o.ToShortDateString())
                .Build(_tableDefinition, ColumnRenderParts.Cell, model).ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual(string.Format("<td>{0}</td>", dt.ToShortDateString()), builderResult);
		}
	}
}
