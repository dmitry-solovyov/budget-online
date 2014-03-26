using System;
using System.Web;
using System.Web.WebPages;

namespace BudgetOnline.UI.Controls
{
	public class FormBuilder : IBuilder
	{
		private Func<HtmlString> _bodyBuilder;
        private Func<HtmlString> _actionsContentBuilder;

		private readonly ContainerBuilder _builder = new ContainerBuilder();

		public FormBuilder()
		{
			_builder
				.CollapseEmptyTags(false)
				.HideIfEmpty(true)
				.Tag("form")
				.Attr("method", "post");
		}

		//public new static FormBuilder Create()
		//{
		//    return new FormBuilder();
		//}

		protected string _id;
		public FormBuilder Id(string id)
		{
			_id = id;
			return this;
		}

		protected string _headerCss;
		public FormBuilder HeaderCss(string css)
		{
			_headerCss = css;
			return this;
		}

		protected string _header;
		public FormBuilder Header(string header)
		{
			_header = header;
			return this;
		}

		public FormBuilder Content(Func<object, HelperResult> content)
		{
			_bodyBuilder = () => new HtmlString(content.Invoke(null).ToHtmlString());
			return this;
		}

        public FormBuilder ActionsContent(Func<object, HelperResult> content)
        {
            _actionsContentBuilder = () => new HtmlString(content.Invoke(null).ToHtmlString());
            return this;
        }

		protected string _actionUrl;
		public FormBuilder ActionUrl(string actionUrl)
		{
			_actionUrl = actionUrl;
			return this;
		}

		protected string _submitClientAction;
		public FormBuilder SubmitClientAction(string action)
		{
			_submitClientAction = action;
			return this;
		}

		protected string _cancelClientAction;
		public FormBuilder CancelClientAction(string action)
		{
			_cancelClientAction = action;
			return this;
		}

		public FormBuilder Css(string css)
		{
			_builder.Css(css);
			return this;
		}

		protected virtual string GetHeaderContent()
		{
			return !string.IsNullOrWhiteSpace(_header)
					? string.Format(@"<h2 class=""{1}"">{0}</h2>", _header, _headerCss)
					: null;
		}

		protected virtual string GetBodyContent()
		{
			return _bodyBuilder != null
				? _bodyBuilder().ToHtmlString()
				: null;
		}

        protected virtual string GetActionsContent()
        {
            if(_actionsContentBuilder != null)
            {
                return string.Format("<div class=\"form-group clearfix\"><div class=\"col-md-9 col-md-offset-2\">{0}</div></div>", _actionsContentBuilder().ToHtmlString());
            }

            return null;
        }

		public virtual HtmlString Build()
		{
			if (!string.IsNullOrWhiteSpace(_actionUrl))
				_builder.Attr("action", _actionUrl);

			if (!string.IsNullOrWhiteSpace(_id))
				_builder.Attr("id", _id);

			if (!string.IsNullOrWhiteSpace(_cancelClientAction))
			{
			}

			var headerContent = GetHeaderContent();
			var bodyContent = GetBodyContent();
		    var actionsContent = GetActionsContent();

			var headerBodySeparator = !string.IsNullOrWhiteSpace(headerContent) && !string.IsNullOrWhiteSpace(bodyContent)
										? Environment.NewLine
										: string.Empty;

			_builder.Content(() => new HtmlString(
				string.Format(
					"{0}{1}{2}{3}",
					headerContent,
					headerBodySeparator,
					bodyContent,
                    actionsContent
				)
			));

			return _builder.Build();
		}
	}
}
