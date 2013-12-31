using System.Web;
using BudgetOnline.UI.Controls;
using BudgetOnline.UI.Controls.Buttons;

namespace BudgetOnline.Web.UI.Controls
{
	public class WebButtonBuilder : ButtonBuilder
	{
		public override HtmlString Build()
		{
			Css(Constants.CSS_CLASS_PREFIX + "btn btn-large btn-primary");

			return base.Build();
		}
	}
}
