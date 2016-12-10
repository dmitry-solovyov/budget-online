
using System.Web;
using BudgetOnline.UI.Models;

namespace BudgetOnline.UI.Controls
{
	public class ProgressIndicatorBuilder : IBuilder
	{
		protected UIBuilder Builder;

		protected ProgressIndicatorModel Model = new ProgressIndicatorModel();
		private string _indicatorUrl;
		private string _class;
		private string _backgroundUrl;

		public static ProgressIndicatorBuilder Create()
		{
			return new ProgressIndicatorBuilder();
		}

		public virtual ProgressIndicatorBuilder Name(string name)
		{
			Model.Name = name;

			return this;
		}

		public virtual ProgressIndicatorBuilder Class(string @class)
		{
			Model.Class = @class;

			return this;
		}

		public virtual ProgressIndicatorBuilder IndicatorUrl(string url)
		{
			Model.IndicatorUrl = url;

			return this;
		}

		public virtual ProgressIndicatorBuilder BackgroundUrl(string url)
		{
			Model.BackgroundUrl = url;

			return this;
		}


		public virtual HtmlString Build()
		{
            return new HtmlString("");
			//var container = new _Views_ProgressIndicator_cshtml();
			//return new HtmlString(container.Render(Model).ToHtmlString());
		}
	}
}
