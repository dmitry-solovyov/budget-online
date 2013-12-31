using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.UI.Controls.Tests.Table
{
	[TestClass]
	public class TableIconColumnBuilderTest
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
		public void TableIconColumnBuilderTest_BuildHeader()
		{
			var model = new Model { F1 = "AA", F2 = 12345 };
			var builderResult = new TableIconColumnBuilder<Model>()
                .Build(_tableDefinition, ColumnRenderParts.Header, model).ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<th></th>", builderResult);
		}

		[TestMethod]
		public void TableIconColumnBuilderTest_BuildCell()
		{
			var model = new Model { F1 = "AA", F2 = 12345 };
			var builderResult = new TableIconColumnBuilder<Model>()
				.IconCss(m => m.F2 > 0 ? "plus" : "minus")
                .Build(_tableDefinition, ColumnRenderParts.Cell, model).ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<td><i class=\"plus\"></i></td>", builderResult);
		}

		//[TestMethod]
		//public void TableIconColumnBuilderTest_BuildHeaderWithClass()
		//{
		//    var model = new Model();
		//    var builderResult = new TableIconColumnBuilder<Model>().Binding(m => m.F1)
		//        .HeaderCss("css")
		//        .Build(ColumnPartTupes.Header).ToHtmlString();

		//    Console.WriteLine(builderResult);

		//    Assert.AreEqual(@"<th class=""css"">Ff 11</th>", builderResult);
		//}

		//[TestMethod]
		//public void TableIconColumnBuilderTest_BuildCell()
		//{
		//    var model = new Model();
		//    var builderResult = new TableIconColumnBuilder<Model>().Binding(m => m.F1)
		//        .Build(ColumnPartTupes.Cell, model).ToHtmlString();

		//    Console.WriteLine(builderResult);

		//    Assert.AreEqual("<td></td>", builderResult);
		//}

		//[TestMethod]
		//public void TableIconColumnBuilderTest_BuildCellWithHeader()
		//{
		//    var model = new Model();
		//    var builderResult = new TableIconColumnBuilder<Model>().Binding(m => m.F1)
		//        .CellCss("css")
		//        .Build(ColumnPartTupes.Cell, model).ToHtmlString();

		//    Console.WriteLine(builderResult);

		//    Assert.AreEqual("<td class=\"css\"></td>", builderResult);
		//}
	}
}
