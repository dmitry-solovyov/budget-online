using System;
using System.Web;
using System.Web.WebPages;

namespace BudgetOnline.UI.Controls
{
	public class PanelBuilder : IBuilder
	{
		protected readonly UIBuilder UiBuilder = new UIBuilder().CollapseEmptyTags(true);

		public PanelBuilder()
		{
			_suppressHeaderIfEmpty = true;
			_suppressFooterIfEmpty = true;
			_suppressContentIfEmpty = false;
		}

		public static PanelBuilder Create()
		{
			return new PanelBuilder();
		}

		protected ContainerBuilder HeaderBuilder = new ContainerBuilder().CollapseEmptyTags(true);
		protected ContainerBuilder ContentBuilder = new ContainerBuilder().CollapseEmptyTags(true);
		protected ContainerBuilder FooterBuilder = new ContainerBuilder().CollapseEmptyTags(true);

		private bool _suppressHeaderIfEmpty;
		private bool _suppressFooterIfEmpty;
		private bool _suppressContentIfEmpty;

		public PanelBuilder SuppressFooterIfEmpty(bool isSupress)
		{
			_suppressFooterIfEmpty = isSupress;
			return this;
		}

		public PanelBuilder SuppressHeaderIfEmpty(bool isSupress)
		{
			_suppressHeaderIfEmpty = isSupress;
			return this;
		}

		public PanelBuilder SuppressContentIfEmpty(bool isSupress)
		{
			_suppressContentIfEmpty = isSupress;
			return this;
		}

		#region Header

		public PanelBuilder Header(string html)
		{
			HeaderBuilder.Content(html);
			return this;
		}

		public PanelBuilder Header(Func<string> builder)
		{
			HeaderBuilder.Content(builder);
			return this;
		}

		public PanelBuilder Header(Func<HtmlString> builder)
		{
			if (builder != null)
				HeaderBuilder.Content(builder);
			return this;
		}

		public PanelBuilder Header(IBuilder builder)
		{
			HeaderBuilder.Content(builder);
			return this;
		}

		#endregion


		#region Footer

		public PanelBuilder Footer(string html)
		{
			FooterBuilder.Content(html);
			return this;
		}

		public PanelBuilder Footer(Func<string> builder)
		{
			if (builder != null)
				FooterBuilder.Content(builder);
			return this;
		}

		public PanelBuilder Footer(Func<HtmlString> builder)
		{
			FooterBuilder.Content(builder);
			return this;
		}

		public PanelBuilder Footer(IBuilder builder)
		{
			FooterBuilder.Content(builder);
			return this;
		}

		#endregion


		#region Content

		public PanelBuilder Content(string html)
		{
			ContentBuilder.Content(html);
			return this;
		}

		public PanelBuilder Content(HtmlString html)
		{
			ContentBuilder.Content(html);
			return this;
		}

		public PanelBuilder Content(Func<string> builder)
		{
			ContentBuilder.Content(builder);
			return this;
		}

		public PanelBuilder Content(Func<object, HelperResult> result)
		{
			ContentBuilder.Content(result);
			return this;
		}
		
		public PanelBuilder Content(Func<HtmlString> builder)
		{
			ContentBuilder.Content(builder);
			return this;
		}

		public PanelBuilder Content(IBuilder builder)
		{
			ContentBuilder.Content(builder);
			return this;
		}

		#endregion

		public HtmlString Build()
		{
			UiBuilder.Tag("div").Css("bo-box");

			if (!HeaderBuilder.IsEmpty() || !_suppressHeaderIfEmpty)
				UiBuilder.Child(() => HeaderBuilder.Css("header"));

			if (!ContentBuilder.IsEmpty() || !_suppressContentIfEmpty)
				UiBuilder.Child(() => ContentBuilder.Css("content"));

			if (!FooterBuilder.IsEmpty() || !_suppressFooterIfEmpty)
				UiBuilder.Child(() => FooterBuilder.Css("footer"));

			return UiBuilder.Build();
		}
	}
}
