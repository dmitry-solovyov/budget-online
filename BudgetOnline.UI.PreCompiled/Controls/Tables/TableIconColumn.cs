using System;
using System.Web;

namespace BudgetOnline.UI.PreCompiled.Controls.Tables
{
	public class TableIconColumnBuilder<T> : TableBaseColumnBuilder<T, TableIconColumnBuilder<T>>
		where T : class
	{
		public TableIconColumnBuilder()
		{
		}

		public TableIconColumnBuilder(Func<T, object> cellGetter)
		{
			_iconCssGetter = cellGetter;
		}

		protected Func<T, object> _iconCssGetter;
		public virtual TableIconColumnBuilder<T> IconCss(Func<T, object> getter)
		{
			_iconCssGetter = getter;

			return this;
		}

		protected Func<T, object> _headerGetter;
		public virtual TableIconColumnBuilder<T> Header(Func<T, object> headerGetter)
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

		protected override HtmlString BuildCell(TableDefinitions tableDefinition,  T context)
		{
			object value = string.Empty;

			if (_iconCssGetter != null)
				value = _iconCssGetter.Invoke(context);

            return new HtmlString(string.Format("<{2}{1}><i class=\"glyphicon {0}\"></i></{2}>", value, GetCellClass(tableDefinition, context), tableDefinition.BodyCellTag));
		}
	}
}
