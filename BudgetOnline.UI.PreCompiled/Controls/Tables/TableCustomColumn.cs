using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.WebPages;

namespace BudgetOnline.UI.PreCompiled.Controls.Tables
{
	public class TableCustomColumnBuilder<T> : TableBaseColumnBuilder<T, TableCustomColumnBuilder<T>>
		where T : class
	{

		public TableCustomColumnBuilder()
		{
		}

		public TableCustomColumnBuilder(Func<T, object> cellGetter)
		{
			_cellGetter = cellGetter;
		}

		protected Func<T, object> _cellGetter;
		public virtual TableCustomColumnBuilder<T> Cell(Func<T, object> getter)
		{
			_cellGetter = getter;

			return this;
		}

		private Expression<Func<HtmlString>> _bodyBuilder;
		public virtual TableCustomColumnBuilder<T> Cell(Expression<Func<T, HelperResult>> content)
		{
			_bodyBuilder = () => new HtmlString(content.Compile().Invoke(null).ToHtmlString());
			return this;
		}


		protected Func<T, object> _headerGetter;
		public virtual TableCustomColumnBuilder<T> Header(Func<T, object> headerGetter)
		{
			_headerGetter = headerGetter;

			return this;
		}

        protected override HtmlString BuildHeader(TableDefinitions tableDefinition, T context)
		{
			object value = string.Empty;

			if (_headerGetter != null)
				value = _headerGetter.Invoke(context);

            return new HtmlString(string.Format("<{2}{1}>{0}</{2}>", value, GetHeaderClass(tableDefinition, context), tableDefinition.HeaderCellTag));

		}

        protected override HtmlString BuildCell(TableDefinitions tableDefinition, T context)
		{
			object value = string.Empty;

			if (_bodyBuilder != null)
				value = _bodyBuilder.Compile().Invoke().ToHtmlString();
			else
				if (_cellGetter != null)
					value = _cellGetter.Invoke(context);

            return new HtmlString(string.Format("<{2}{1}>{0}</{2}>", value, GetCellClass(tableDefinition, context), tableDefinition.BodyRowTag));
		}
	}
}
