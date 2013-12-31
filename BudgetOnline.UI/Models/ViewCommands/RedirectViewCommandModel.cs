namespace BudgetOnline.UI.Models.ViewCommands
{
	public class RedirectViewCommandModel : ViewCommandModel
	{
		public RedirectViewCommandModel()
		{
			CommandType = CommandType.Redirect;
		}

		public string Path { get; set; }

		public override string ToString()
		{
			return string.Empty;
		}
	}
}