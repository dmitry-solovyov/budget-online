using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BudgetOnline.Common;

namespace BudgetOnline.UI.Controls.Tables
{
	public class TableBuilder<T> : IBuilder
		where T : class
	{
		private readonly ContainerBuilder _builder = new ContainerBuilder();

		public TableBuilder()
		{
			_builder
				.CollapseEmptyTags(false)
				.HideIfEmpty(true);
		}

		public TableDefinitions TableDefinitions
		{
			get
			{
				if (_outputType == TableBuilderOutputTypes.Div)
					return new DivAsTableDefinitions();

				return new TableDefinitions();
			}
		}

		protected string _id;
		public TableBuilder<T> Id(string id)
		{
			_id = id;
			return this;
		}

		protected string _headerCss;
		public TableBuilder<T> HeaderCss(string css)
		{
			_headerCss = css;
			return this;
		}

		protected string _header;
		public TableBuilder<T> Header(string header)
		{
			_header = header;
			return this;
		}

		private string _tableCss;
		public TableBuilder<T> Css(string css)
		{
			_tableCss = css;
			return this;
		}

		private Action<TableColumnCollection<T>> _columns;
		public TableBuilder<T> Columns(Action<TableColumnCollection<T>> columns)
		{
			_columns = columns;
			return this;
		}

		private IEnumerable<T> _rows;
		public TableBuilder<T> Rows(IEnumerable<T> rows)
		{
			_rows = rows;
			return this;
		}

		private Func<T, string> _rowStyleRetriever;
		public TableBuilder<T> RowStyleRetriever(Func<T, string> func)
		{
			_rowStyleRetriever = func;
			return this;
		}

		private bool _suppressHeader;
		public TableBuilder<T> SupressHeader()
		{
			_suppressHeader = true;
			return this;
		}

		private TableBuilderOutputTypes _outputType = TableBuilderOutputTypes.Table;
		public TableBuilder<T> OutputType(TableBuilderOutputTypes outputType)
		{
			_outputType = outputType;
			return this;
		}

		protected virtual string GetHeaderContent()
		{
			return !string.IsNullOrWhiteSpace(_header)
					? string.Format(@"<h2 class=""{1}"">{0}</h2>", _header, _headerCss)
					: null;
		}

		public virtual HtmlString Build()
		{
			if (!string.IsNullOrWhiteSpace(_id))
				_builder.Attr("id", _id);

			var columnBuilders = RetrieveColumnBuilders().ToList();

			var table = string.Format("<{0}{1}>{2}{3}{4}{5}</{0}>",
				TableDefinitions.TableTag,
				GetTableCss(),
				BuildTableCaption(),
				BuildColWidths(),
				BuildTableHeaders(columnBuilders),
				BuildTableBody(columnBuilders)
				);

			return new HtmlString(table);
		}


		private IEnumerable<IMultiBuilder<ColumnRenderParts, T>> RetrieveColumnBuilders()
		{
			var columnCollection = new TableColumnCollection<T>();

			_columns.Invoke(columnCollection);

			return columnCollection.Columns;
		}

		private string BuildTableHeaders(IEnumerable<IMultiBuilder<ColumnRenderParts, T>> columnBuilders)
		{
			if (!_suppressHeader)
			{
				var tds = new List<string>(5);
				tds.AddRange(columnBuilders.Select(column => column.Build(TableDefinitions, ColumnRenderParts.Header).ToHtmlString()));
				return string.Format(
					"<{0}><{1}>{2}</{1}></{0}>",
					TableDefinitions.HeaderSectionTag,
					TableDefinitions.HeaderRowTag,
					tds.JoinedString());
			}

			return string.Empty;
		}

		private string BuildTableBody(IEnumerable<IMultiBuilder<ColumnRenderParts, T>> columnBuilders)
		{
			var trs = new List<string>(5);
			foreach (var row in _rows)
			{
				var tds = new List<string>(5);
				tds.AddRange(columnBuilders.Select(column => column.Build(TableDefinitions, ColumnRenderParts.Cell, row).ToHtmlString()));

				string rowStyle = string.Empty;
				if (_rowStyleRetriever != null)
				{
					rowStyle = _rowStyleRetriever(row);
					if (!string.IsNullOrWhiteSpace(rowStyle))
						rowStyle = string.Format(" class=\"{0}\"", rowStyle);
				}

				trs.Add(string.Format("<{2}{1}>{0}</{2}>", tds.JoinedString(), rowStyle, TableDefinitions.BodyRowTag));
			}

			return string.Format("<{1}>{0}</{1}>", trs.JoinedString(), TableDefinitions.BodySectionTag);
		}

		private string BuildTableCaption()
		{
			return string.Empty;
		}

		private string GetTableCss()
		{
			if (!string.IsNullOrWhiteSpace(_tableCss))
			{
				return string.Format(" class=\"{0}\"", _tableCss);
			}

			return string.Empty;
		}

		private string BuildColWidths()
		{
			//<colgroup>
			//  <col span="2" style="background-color:red">
			//  <col style="background-color:yellow">
			//</colgroup>
			return string.Empty;
		}
	}

	public enum TableBuilderOutputTypes
	{
		Table,
		Div
	}

	public class TableDefinitions
	{
		public virtual string TableTag { get { return "table"; } }
		public virtual string TableClass { get { return ""; } }

		public virtual string BodyRowTag { get { return "tr"; } }
		public virtual string BodyRowClass { get { return ""; } }
		public virtual string BodyCellTag { get { return "td"; } }
		public virtual string BodyCellClass { get { return ""; } }
		public virtual string BodySectionTag { get { return "tbody"; } }
		public virtual string BodySectionClass { get { return ""; } }

		public virtual string HeaderRowTag { get { return "tr"; } }
		public virtual string HeaderRowClass { get { return ""; } }
		public virtual string HeaderCellTag { get { return "th"; } }
		public virtual string HeaderCellClass { get { return ""; } }
		public virtual string HeaderSectionTag { get { return "thead"; } }
		public virtual string HeaderSectionClass { get { return ""; } }
	}

	public class DivAsTableDefinitions : TableDefinitions
	{
		public override string TableTag { get { return "div"; } }
		public override string TableClass { get { return ""; } }

		public override string BodyRowTag { get { return "div"; } }
		public override string BodyRowClass { get { return "full-row"; } }
		public override string BodyCellTag { get { return "div"; } }
		public override string BodyCellClass { get { return ""; } }
		public override string BodySectionTag { get { return "div"; } }
		public override string BodySectionClass { get { return "tbody"; } }

		public override string HeaderRowTag { get { return "div"; } }
		public override string HeaderRowClass { get { return "full-row"; } }
		public override string HeaderCellTag { get { return "div"; } }
		public override string HeaderCellClass { get { return "th"; } }
		public override string HeaderSectionTag { get { return "div"; } }
		public override string HeaderSectionClass { get { return "thead"; } }
	}
}
