using System;
using System.Collections.Generic;
using System.Web;
using ASP;
using BudgetOnline.UI.Models.ViewCommands;

namespace BudgetOnline.UI.Controls
{
	public class CommandsListBuilder : IBuilder
	{
		protected string _id;
		public CommandsListBuilder Id(string id)
		{
			_id = id;
			return this;
		}

		private Func<IEnumerable<ViewCommandUIModel>> _commands;
		public CommandsListBuilder Commands(Func<IEnumerable<ViewCommandUIModel>> commands)
		{
			_commands = commands;
			return this;
		}

		public virtual HtmlString Build()
		{
			if (_commands == null)
				return new HtmlString(string.Empty);

            var render = new _Views_ListViewCommands_ListOfViewCommandUI_cshtml().Render(_commands());

			return new HtmlString(render.ToHtmlString());
		}
	}
}
