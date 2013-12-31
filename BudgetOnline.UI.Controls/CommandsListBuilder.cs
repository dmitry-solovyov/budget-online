using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
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

			var render = Views.ListViewCommands.ListOfViewCommandUI.Render(_commands());

			return new HtmlString(render.ToHtmlString());
		}
	}
}
