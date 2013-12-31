using System;

namespace BudgetOnline.UI.Models.ViewCommands
{
	public class ViewCommandModel
	{
		public CommandType CommandType { get; protected set; }

		public string Data { get; set; }
	}

	public enum CommandType
	{
		Js,
		Ajax,
		Post,
		Redirect
	}
}
