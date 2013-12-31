using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BudgetOnline.UI.Controls
{
	public class TableColumnCollection<T>
		where T : class
	{
		private List<IMultiBuilder<ColumnRenderParts, T>> _columns;
		public List<IMultiBuilder<ColumnRenderParts, T>> Columns
		{
			get { return _columns ?? (_columns = new List<IMultiBuilder<ColumnRenderParts, T>>()); }
		}

		public TableBoundColumnBuilder<T> Bound(Expression<Func<T, object>> binding)
		{
			var newCol = new TableBoundColumnBuilder<T>(binding);

			Columns.Add(newCol);

			return newCol;
		}

		public TableIconColumnBuilder<T> Icon()
		{
			var newCol = new TableIconColumnBuilder<T>();

			Columns.Add(newCol);

			return newCol;
		}

		public TableCustomColumnBuilder<T> Custom()
		{
			var newCol = new TableCustomColumnBuilder<T>();

			Columns.Add(newCol);

			return newCol;
		}

		public TableCommandsColumnBuilder<T> Commands()
		{
			var newCol = new TableCommandsColumnBuilder<T>();

			Columns.Add(newCol);

			return newCol;
		}
	}
}
