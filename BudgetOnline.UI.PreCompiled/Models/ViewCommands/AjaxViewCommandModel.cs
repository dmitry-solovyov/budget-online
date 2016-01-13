using System;

namespace BudgetOnline.UI.PreCompiled.Models.ViewCommands
{
	public class AjaxViewCommandModel : ViewCommandModel
	{
		public AjaxViewCommandModel()
		{
			CommandType = CommandType.Ajax;
		}

		public Uri Path { get; set; }
		public string Data { get; set; }
	}
}