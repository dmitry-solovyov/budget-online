using System;
using System.Collections.Generic;
using System.Web;
using ASP;
using BudgetOnline.UI.PreCompiled.Models.ViewCommands;

namespace BudgetOnline.UI.PreCompiled.Controls.Tables
{
	public sealed class TableCommandsColumnBuilder<T> : TableBaseColumnBuilder<T, TableCommandsColumnBuilder<T>>
		where T : class
	{
		public TableCommandsColumnBuilder()
		{
		}

		public TableCommandsColumnBuilder(Func<T, IEnumerable<ViewCommandUIModel>> commandGetter)
		{
			_commandGetter = commandGetter;
		}

	    private Func<T, IEnumerable<ViewCommandUIModel>> _commandGetter;
		public TableCommandsColumnBuilder<T> Cell(Func<T, IEnumerable<ViewCommandUIModel>> commandGetter)
		{
			_commandGetter = commandGetter;

			return this;
		}

		protected override HtmlString BuildHeader(TableDefinitions tableDefinition, T context)
		{
            return new HtmlString(string.Format("<{2}{1}>{0}</{2}>", "Команды", GetHeaderClass(tableDefinition, context), tableDefinition.HeaderCellTag));

		}

        protected override HtmlString BuildCell(TableDefinitions tableDefinition, T context)
		{
			var value = new _Page_Views_ListViewCommands_ListOfViewCommandUI_cshtml().Render(_commandGetter.Invoke(context)).ToHtmlString();

			//if (_cellGetter != null)
			//	value = _cellGetter.Invoke(context);

            return new HtmlString(string.Format("<{2}{1}><div class=\"pull-right\">{0}</div></{2}>", value, GetCellClass(tableDefinition, context), tableDefinition.BodyCellTag));
		}
	}
}
