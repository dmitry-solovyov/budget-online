using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BudgetOnline.Common;

namespace BudgetOnline.UI.PreCompiled.Models.ViewCommands
{
	public abstract class ViewCommandUiModelHelper
	{
		private string _caption;
		public ViewCommandUiModelHelper Caption(string text)
		{
			_caption = text;
			return this;
		}

		private string _title;
		public ViewCommandUiModelHelper Title(string title)
		{
			_title = title;
			return this;
		}

		private string _action;
		public ViewCommandUiModelHelper Action(string action)
		{
			_action = action;
			return this;
		}

		private string _controller;
		public ViewCommandUiModelHelper Controller(string controller)
		{
			_controller = controller;
			return this;
		}

		private string _area;
		public ViewCommandUiModelHelper Area(string area)
		{
			_area = area;
			return this;
		}

		private ViewCommandModel _command;
		public ViewCommandUiModelHelper Command(ViewCommandModel command)
		{
			_command = command;
			return this;
		}

		private bool _dividerBefore;
		public ViewCommandUiModelHelper DividerBefore(bool dividerBefore)
		{
			_dividerBefore = dividerBefore;
			return this;
		}

		private bool _dividerAfter;
		public ViewCommandUiModelHelper DividerAfter(bool dividerAfter)
		{
			_dividerAfter = dividerAfter;
			return this;
		}

		private NavigationHelper.ShowActiveLevels _activeLevel;
		public ViewCommandUiModelHelper ShowActiveLevel(NavigationHelper.ShowActiveLevels activeLevel)
		{
			_activeLevel = activeLevel;
			return this;
		}

		private IEnumerable<ViewCommandUiModelHelper> _childItems = new ViewCommandUiModelHelper[0];
		public ViewCommandUiModelHelper Child(IEnumerable<ViewCommandUiModelHelper> childItems)
		{
			_childItems = childItems;
			return this;
		}

		private string _iconClass;
		public ViewCommandUiModelHelper IconClass(string iconClass)
		{
			_iconClass = iconClass;
			return this;
		}

		public ViewCommandUIModel GetResult()
		{
			return new ViewCommandUIModel
                            {
                                Text = _caption,
								Command = _command ?? new RedirectViewCommandModel { Path = new UrlHelper(HttpContext.Current.Request.RequestContext).Action(_action, _controller, new { area = _area }) },
                                Title = _title,
                                IsDividerBefore = _dividerBefore,
                                IsDividerAfter = _dividerAfter,
                                IconCssClass = _iconClass,
                                //IconCssClass = NavigationHelper.IsPathActive(NavigationHelper.ShowActiveLevels.ActionLevel, _controller, _action, _area) ? "icon-white" : "",
                                ChildCommands = _childItems.Select(o => o.GetResult())
                            };
		}


	}
}