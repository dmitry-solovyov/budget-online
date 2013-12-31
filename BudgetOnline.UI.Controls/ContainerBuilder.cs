using System;
using System.Web;
using System.Web.WebPages;

namespace BudgetOnline.UI.Controls
{
	public class ContainerBuilder : IBuilder
	{
		protected readonly UIBuilder UiBuilder = new UIBuilder();

		public ContainerBuilder Content(string html)
		{
			UiBuilder.Content(() => new HtmlString(html));
			return this;
		}

		public ContainerBuilder Content(HtmlString html)
		{
			UiBuilder.Content(() => html);
			return this;
		}

		public ContainerBuilder Content(Func<string> builder)
		{
			UiBuilder.Content(() => new HtmlString(builder()));
			return this;
		}

		public ContainerBuilder Content(Func<object, HelperResult> result)
		{
			UiBuilder.Content(() => new HtmlString(result.Invoke(null).ToHtmlString()));
			return this;
		}

		public ContainerBuilder Content(Func<HtmlString> builder)
		{
			UiBuilder.Content(builder);
			return this;
		}

		public ContainerBuilder Content(IBuilder builder)
		{
			UiBuilder.Content(builder.Build);
			return this;
		}

		public ContainerBuilder Tag(string tag)
		{
			UiBuilder.Tag(tag);
			return this;
		}

		public ContainerBuilder Css(string css)
		{
			UiBuilder.Css(css);
			return this;
		}

		public ContainerBuilder Attr(string attr, string value)
		{
			UiBuilder.Attr(attr, value);
			return this;
		}

		public ContainerBuilder HideIfEmpty(bool hideIfEmpty)
		{
			UiBuilder.HideIfEmpty(hideIfEmpty);
			return this;
		}

		protected bool CollapseEmptyTagsSetting = true;
		public ContainerBuilder CollapseEmptyTags(bool collapseEmptyTags)
		{
			CollapseEmptyTagsSetting = collapseEmptyTags;
			return this;
		}

		public virtual bool IsEmpty()
		{
			return UiBuilder.IsEmpty();
		}

		public virtual HtmlString Build()
		{
			//if (IsEmpty())
			//    return new HtmlString(string.Empty);

			UiBuilder
				.CollapseEmptyTags(CollapseEmptyTagsSetting);

			var s = UiBuilder.Build();
			return s;
		}
	}
}
