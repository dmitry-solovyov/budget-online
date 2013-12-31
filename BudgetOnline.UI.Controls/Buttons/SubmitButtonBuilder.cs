using System.Web;

namespace BudgetOnline.UI.Controls.Buttons
{
	public class SubmitButtonBuilder : ButtonBuilder
	{
		public override HtmlString Build()
		{
			ButtonType(ButtonTypes.Submit);

			return base.Build();
		}
	}
}
