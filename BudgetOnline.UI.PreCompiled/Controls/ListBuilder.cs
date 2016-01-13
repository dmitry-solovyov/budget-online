using System;
using System.Collections.Generic;
using System.Web;
using System.Web.WebPages;
using BudgetOnline.Common;

namespace BudgetOnline.UI.PreCompiled.Controls
{
	public class ListBuilder<T> : IBuilder
		where T : class
	{
		private readonly ContainerBuilder _builder = new ContainerBuilder();

		public ListBuilder()
		{
			_builder
				.CollapseEmptyTags(false)
				.HideIfEmpty(true)
				.Tag("table");
		}

		protected string _id;
		public ListBuilder<T> Id(string id)
		{
			_id = id;
			return this;
		}

		protected string _headerCss;
		public ListBuilder<T> HeaderCss(string css)
		{
			_headerCss = css;
			return this;
		}


		private string _tableCss;
		public ListBuilder<T> Css(string css)
		{
			_tableCss = css;
			return this;
		}

		private IEnumerable<T> _rows;
		public ListBuilder<T> Rows(IEnumerable<T> rows)
		{
			_rows = rows;
			return this;
		}

		private Func<T, HelperResult> _headerTemplate;
		public ListBuilder<T> HeaderTemplate(Func<T, HelperResult> func)
		{
			_headerTemplate = func;
			return this;
		}

		private Func<T, HelperResult> _rowTemplate;
		public ListBuilder<T> RowTemplate(Func<T, HelperResult> func)
		{
			_rowTemplate = func;
			return this;
		}

		private Func<T, string> _rowStyleRetriever;
		public ListBuilder<T> RowStyleRetriever(Func<T, string> func)
		{
			_rowStyleRetriever = func;
			return this;
		}

		private bool _supressHeader;
		public ListBuilder<T> SupressHeader(bool supress)
		{
			_supressHeader = supress;
			return this;
		}

		public virtual HtmlString Build()
		{
			if (!string.IsNullOrWhiteSpace(_id))
				_builder.Attr("id", _id);

			var table = string.Format("<div class=\"list\">{0}</div>",
				BuildListBody()
				);

			return new HtmlString(table);
		}

		private string BuildListHeader()
		{
			if (!_supressHeader && _headerTemplate != null)
			{
				var headerContent = _headerTemplate(null);
				if (headerContent != null)
					return string.Format(@"{0}", headerContent.ToHtmlString());
			}

			return null;
		}

		private string BuildListBody()
		{
			var rows = new List<string>(5);

			var headerContent = BuildListHeader();
			if (!string.IsNullOrWhiteSpace(headerContent))
				rows.Add(headerContent);

			foreach (var row in _rows)
			{
				string rowContent = string.Empty;
				if (_rowTemplate != null)
				{
					rowContent = _rowTemplate(row).ToHtmlString();
				}

				rows.Add(string.Format("{0}", rowContent));
			}

			return string.Format("{0}", rows.JoinedString());
		}
	}
}
