namespace BudgetOnline.Web.UI.Controls
{
	public class FormBuilder : BudgetOnline.UI.Controls.FormBuilder
	{
		protected override string GetHeaderContent()
		{
			return !string.IsNullOrWhiteSpace(_header)
					? string.Format(@"<h4 class=""{1}"">{0}</h4>", _header, _headerCss)
					: null;
		}
	}
}
