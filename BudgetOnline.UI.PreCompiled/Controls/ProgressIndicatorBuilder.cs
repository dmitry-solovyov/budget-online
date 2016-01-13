using System.Web;
using ASP;
using BudgetOnline.UI.PreCompiled.Models;

namespace BudgetOnline.UI.PreCompiled.Controls
{
	public class ProgressIndicatorBuilder : IBuilder
	{
		protected UIBuilder Builder;

	    private readonly ProgressIndicatorModel _model = new ProgressIndicatorModel();
		private string _indicatorUrl;
		private string _class;
		private string _backgroundUrl;

		public static ProgressIndicatorBuilder Create()
		{
			return new ProgressIndicatorBuilder();
		}

		public virtual ProgressIndicatorBuilder Name(string name)
		{
			_model.Name = name;

			return this;
		}

		public virtual ProgressIndicatorBuilder Class(string @class)
		{
			_model.Class = @class;

			return this;
		}

		public virtual ProgressIndicatorBuilder IndicatorUrl(string url)
		{
			_model.IndicatorUrl = url;

			return this;
		}

		public virtual ProgressIndicatorBuilder BackgroundUrl(string url)
		{
			_model.BackgroundUrl = url;

			return this;
		}


		public virtual HtmlString Build()
		{
			var container = new _Page_Views_ProgressIndicator_cshtml();
			return new HtmlString(container.Render(_model).ToHtmlString());
		}
	}
}
