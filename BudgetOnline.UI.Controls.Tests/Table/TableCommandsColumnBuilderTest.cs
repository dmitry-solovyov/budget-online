using System;
using System.Collections.Generic;
using System.ComponentModel;
using BudgetOnline.UI.Models.ViewCommands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetOnline.UI.Controls.Tests.Table
{
	[TestClass]
	public class TableCommandsColumnBuilderTest
	{
        readonly TableDefinitions _tableDefinition = new TableDefinitions();
        class Model
		{
			[DisplayName("Col1")]
			public string F1 { get; set; }
			[DisplayName("Col2")]
			public int F2 { get; set; }

			public IEnumerable<ViewCommandUIModel> F3 { get; set; }
		}

		[TestMethod]
		public void TableCommandsColumnBuilderTest_BuildHeader()
		{
			var model = new Model { F1 = "AA", F2 = 12345 };
			var builderResult = new TableCommandsColumnBuilder<Model>()
                .Build(_tableDefinition, ColumnRenderParts.Header, model).ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual("<th>Команды</th>", builderResult);
		}

		[TestMethod]
		public void TableCommandsColumnBuilderTest_BuildCell()
		{
			var model = new Model
			{
				F1 = "AA",
				F2 = 12345,
				F3 = new[] { new ViewCommandUIModel
				           	{
				           		IsDefault = true,
								Command = new RedirectViewCommandModel{ Path = "http://localhost" },
								Text = "Redirect"
				           	} 
						}
			};
			var builderResult = new TableCommandsColumnBuilder<Model>()
				.Cell(m => m.F3)
                .Build(_tableDefinition, ColumnRenderParts.Cell, model).ToHtmlString();

			Console.WriteLine(builderResult);

			Assert.AreEqual(@"<td><div class=""pull-right"">    <div class=""btn-group"">
<a class=""btn btn-mini"" href=""http://localhost""><i class=""icon-list""></i><span style=""padding-left: 1em"">Redirect</span></a>
        <a class=""btn btn-mini dropdown-toggle"" data-toggle=""dropdown"" href=""#""><span class=""caret""></span></a>
        <ul class=""dropdown-menu"">
    <li class=""t4"">    <a href=""http://localhost""><i class=""i""></i>Redirect</a>
</li>
        </ul>
    </div>
</div></td>", builderResult, true);
		}

		//[TestMethod]
		//public void TableCommandsColumnBuilderTest_BuildHeaderWithClass()
		//{
		//    var model = new Model();
		//    var builderResult = new TableCommandsColumnBuilder<Model>().Binding(m => m.F1)
		//        .HeaderCss("css")
		//        .Build(ColumnPartTupes.Header).ToHtmlString();

		//    Console.WriteLine(builderResult);

		//    Assert.AreEqual(@"<th class=""css"">Ff 11</th>", builderResult);
		//}

		//[TestMethod]
		//public void TableCommandsColumnBuilderTest_BuildCell()
		//{
		//    var model = new Model();
		//    var builderResult = new TableCommandsColumnBuilder<Model>().Binding(m => m.F1)
		//        .Build(ColumnPartTupes.Cell, model).ToHtmlString();

		//    Console.WriteLine(builderResult);

		//    Assert.AreEqual("<td></td>", builderResult);
		//}

		//[TestMethod]
		//public void TableCommandsColumnBuilderTest_BuildCellWithHeader()
		//{
		//    var model = new Model();
		//    var builderResult = new TableCommandsColumnBuilder<Model>().Binding(m => m.F1)
		//        .CellCss("css")
		//        .Build(ColumnPartTupes.Cell, model).ToHtmlString();

		//    Console.WriteLine(builderResult);

		//    Assert.AreEqual("<td class=\"css\"></td>", builderResult);
		//}
	}
}
