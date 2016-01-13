using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace BudgetOnline.UI.PreCompiled.Extensions
{
	public static class HtmlLabelForExtensions
	{
		public static MvcHtmlString LocalLabel(this HtmlHelper htmlHelper, string forName, string labelText)
		{
			return LocalLabel(htmlHelper, forName, labelText, (object)null);
		}

		public static MvcHtmlString LocalLabel(this HtmlHelper htmlHelper, string forName, string labelText,
										  object htmlAttributes)
		{
			return LocalLabel(htmlHelper, forName, labelText, new RouteValueDictionary(htmlAttributes));
		}

		public static MvcHtmlString LocalLabel(this HtmlHelper htmlHelper, string forName, string labelText,
										  IDictionary<string, object> htmlAttributes)
		{
			var tagBuilder = new TagBuilder("label");
			tagBuilder.MergeAttributes(htmlAttributes);
			tagBuilder.MergeAttribute("for", forName.Replace(".", tagBuilder.IdAttributeDotReplacement), true);
			tagBuilder.SetInnerText(labelText);
			return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
		}

		public static MvcHtmlString LocalLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
																Expression<Func<TModel, TProperty>> expression,
																string labelText)
		{
			return LocalLabelFor(htmlHelper, expression, labelText, (object)null);
		}

		public static MvcHtmlString LocalLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
																Expression<Func<TModel, TProperty>> expression,
																string labelText, object htmlAttributes)
		{
			return LocalLabelFor(htmlHelper, expression, labelText, new RouteValueDictionary(htmlAttributes));
		}

		public static MvcHtmlString LocalLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
																Expression<Func<TModel, TProperty>> expression,
																string labelText,
																IDictionary<string, object> htmlAttributes)
		{
			string inputName = ExpressionHelper.GetExpressionText(expression);
			return htmlHelper.LocalLabel(inputName, labelText, htmlAttributes);
		}
	}
}