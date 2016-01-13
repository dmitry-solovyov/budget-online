namespace BudgetOnline.UI.PreCompiled.Models.ViewCommands
{
	public class JsViewCommandModel : ViewCommandModel
	{
		public JsViewCommandModel()
		{
			CommandType = CommandType.Js;
		}

		public string Data { get; set; }
		public string ClientFunction { get; set; }
	}
}