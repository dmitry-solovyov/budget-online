using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.UI.Controls.Tests.Table
{
	[TestClass]
	public class TableCustomColumnBuilderTest
	{
        readonly TableDefinitions _tableDefinition = new TableDefinitions();
        class Model
		{
			[DisplayName("Col1")]
			public string F1 { get; set; }
			[DisplayName("Col2")]
			public int F2 { get; set; }
		}

		[TestMethod]
		public void TableCustomColumnBuilderTest_BuildHeader()
		{
			var model = new Model { F1 = "AA", F2 = 12345 };
			var builderResult = new TableCustomColumnBuilder<Model>()
				.Header(m => m.F1 + " " + m.F1)
				.Cell(m => m.F1)
                .Build(_tableDefinition, ColumnRenderParts.Header, model).ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<th>AA AA</th>", builderResult);
		}

		[TestMethod]
		public void TableCustomColumnBuilderTest_BuildHeaderWithCss()
		{
			var model = new Model { F1 = "AA", F2 = 12345 };
			var builderResult = new TableCustomColumnBuilder<Model>()
				.HeaderCss("css")
				.Header(m => m.F1 + " " + m.F1)
				.Cell(m => m.F1)
                .Build(_tableDefinition, ColumnRenderParts.Header, model).ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<th class=\"css\">AA AA</th>", builderResult);
		}

		//[TestMethod]
		//public void TableCustomColumnBuilderTest_BuildHeaderWithClass()
		//{
		//    var model = new Model();
		//    var builderResult = new TableCustomColumnBuilder<Model>().Binding(m => m.F1)
		//        .HeaderCss("css")
		//        .Build(ColumnPartTupes.Header).ToHtmlString();

		//    Console.WriteLine(builderResult);

		//    Assert.AreEqual(@"<th class=""css"">Ff 11</th>", builderResult);
		//}

		//[TestMethod]
		//public void TableCustomColumnBuilderTest_BuildCell()
		//{
		//    var model = new Model();
		//    var builderResult = new TableCustomColumnBuilder<Model>().Binding(m => m.F1)
		//        .Build(ColumnPartTupes.Cell, model).ToHtmlString();

		//    Console.WriteLine(builderResult);

		//    Assert.AreEqual("<td></td>", builderResult);
		//}

		//[TestMethod]
		//public void TableCustomColumnBuilderTest_BuildCellWithHeader()
		//{
		//    var model = new Model();
		//    var builderResult = new TableCustomColumnBuilder<Model>().Binding(m => m.F1)
		//        .CellCss("css")
		//        .Build(ColumnPartTupes.Cell, model).ToHtmlString();

		//    Console.WriteLine(builderResult);

		//    Assert.AreEqual("<td class=\"css\"></td>", builderResult);
		//}
	}
}
