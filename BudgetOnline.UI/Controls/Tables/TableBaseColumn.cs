using System;
using System.Collections.Generic;
using System.Web;

namespace BudgetOnline.UI.Controls
{
	public class TableBaseColumnBuilder<TModel, TBuilderType> : IMultiBuilder<ColumnRenderParts, TModel>
		where TModel : class
		where TBuilderType : class
	{
		protected string _headerCss;
		public virtual TBuilderType HeaderCss(string css)
		{
			_headerCss = css;

			return this as TBuilderType;
		}

		protected Func<TModel, string> _cellCss;
		public virtual TBuilderType CellCss(Func<TModel, string> cellCss)
		{
			_cellCss = cellCss;

			return this as TBuilderType;
		}

		public virtual TBuilderType CellCss(string cellCss)
		{
			_cellCss = T => cellCss;

			return this as TBuilderType;
		}

		private int _span = 0;
		public virtual TBuilderType Span(int span)
		{
			_span = span;

			return this as TBuilderType;
		}

		public virtual HtmlString Build(TableDefinitions tableDefinitions, ColumnRenderParts type)
		{
			return Build(tableDefinitions, type, null);
		}

		public virtual HtmlString Build(TableDefinitions tableDefinitions, ColumnRenderParts type, TModel context)
		{
			switch (type)
			{
				case ColumnRenderParts.Cell:
					return BuildCell(tableDefinitions, context);
				case ColumnRenderParts.Header:
					return BuildHeader(tableDefinitions, context);
				default:
					break;
			}
			return new HtmlString(string.Empty);
		}

		//public virtual TColumn As<TColumn>()
		//    where TColumn : TableBaseColumnBuilder<TModel>
		//{
		//    var converted = this as TColumn;
		//    if (converted == null)
		//        throw new Exception("Invalid target type");

		//    return converted;
		//}

		#region Protected building stuff

		protected virtual HtmlString BuildHeader(TableDefinitions tableDefinitions, TModel context)
		{
			return new HtmlString(string.Format("<{0}></{0}>", tableDefinitions.HeaderCellTag));
		}

		protected virtual HtmlString BuildCell(TableDefinitions tableDefinitions, TModel context)
		{
			return new HtmlString(string.Format("<{0}></{0}>", tableDefinitions.BodyCellTag));
		}

		protected virtual string GetHeaderClass(TableDefinitions tableDefinitions, TModel context)
		{
			var classes = new List<string>();
			if (!string.IsNullOrWhiteSpace(_headerCss))
				classes.AddRange(_headerCss.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
			if (_span > 0)
				classes.Add(string.Format("span{0}", _span));
			if (!string.IsNullOrWhiteSpace(tableDefinitions.HeaderRowClass))
				classes.AddRange(tableDefinitions.HeaderRowClass.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

			if (classes.Count > 0)
				return " class=\"" + string.Join(" ", classes) + "\"";

			return string.Empty;
		}

		protected virtual string GetCellClass(TableDefinitions tableDefinitions, TModel context)
		{
			var classes = new List<string>();

			if (_cellCss != null && context != null)
				classes.AddRange(_cellCss(context).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

			if (_span > 0)
				classes.Add(string.Format("span{0}", _span));

			if (classes.Count > 0)
				return " class=\"" + string.Join(" ", classes) + "\"";


			return string.Empty;
		}

		#endregion
	}
}
