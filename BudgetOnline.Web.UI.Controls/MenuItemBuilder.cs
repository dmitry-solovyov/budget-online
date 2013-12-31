using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BudgetOnline.UI.Controls;
using BudgetOnline.UI.Models.ViewCommands;
using BudgetOnline.UI.Views.ListViewCommands;

namespace BudgetOnline.Web.UI.Controls
{
	public class MenuItemBuilder : IBuilder
	{
		private string _activeLinkCss;
		internal MenuItemBuilder ActiveLinkCss(string activeLinkCss)
		{
			_activeLinkCss = activeLinkCss;
			return this;
		}


		private string _action;
		public MenuItemBuilder Action(string action)
		{
			_action = action;
			return this;
		}

		private string _controller;
		public MenuItemBuilder Controller(string controller)
		{
			_controller = controller;
			return this;
		}

		private string _area;
		public MenuItemBuilder Area(string area)
		{
			_area = area;
			return this;
		}

		private bool _leftDivider;
		public MenuItemBuilder LeftDivider()
		{
			_leftDivider = true;
			return this;
		}

		private bool _rightDivider;
		public MenuItemBuilder RightDivider()
		{
			_rightDivider = true;
			return this;
		}

		private string _caption;
		public MenuItemBuilder Caption(string caption)
		{
			_caption = caption;
			return this;
		}

		private RouteValueDictionary _routeDictionary = new RouteValueDictionary();
		public MenuItemBuilder RouteDictionary(RouteValueDictionary routeValues)
		{
			_routeDictionary = routeValues;
			return this;
		}

		public MenuItemBuilder AddRouteData(string key, string value)
		{
			_routeDictionary.Add(key, value);
			return this;
		}

		private string _title;
		public MenuItemBuilder Title(string title)
		{
			_title = title;
			return this;
		}

		private string _iconCss;
		public MenuItemBuilder IconCss(string iconCss)
		{
			_iconCss = iconCss;
			return this;
		}

		private bool _showActiveLevelChanged = false;
		private ShowActiveLevels _showActiveLevel = ShowActiveLevels.ControllerAndActionLevel;
		public MenuItemBuilder ShowActiveLevel(ShowActiveLevels level)
		{
			if (!_showActiveLevelChanged)
			{
				_showActiveLevel = level;
				_showActiveLevelChanged = true;
			}
			return this;
		}

		#region Implementation of IBuilder

		public HtmlString Build()
		{
			string leftDiv = string.Empty;
			string rightDiv = string.Empty;

			var li = new TagBuilder("li") { InnerHtml = GetLinkControl() };

			if (IsPathActive() && !string.IsNullOrWhiteSpace(_activeLinkCss))
			{
				li.AddCssClass(_activeLinkCss);
			}

			if (_leftDivider)
			{
				var liLeftDiv = new TagBuilder("li");
				liLeftDiv.AddCssClass("divider-vertical");

				leftDiv = liLeftDiv.ToString();
			}

			return MvcHtmlString.Create(string.Format("{1}{0}{2}", li.ToString(), leftDiv, rightDiv));
		}

		private string GetLinkControl()
		{
			if (!string.IsNullOrWhiteSpace(_area))
				_routeDictionary.Add("Area", _area);

			var innerPart = _caption;
			var a = new TagBuilder("a");
			if (!string.IsNullOrWhiteSpace(_title))
			{
				a.Attributes.Add("title", _title);
			}

			if (!string.IsNullOrWhiteSpace(_iconCss))
				if (IsPathActive())
				{
					innerPart = string.Format(@"<i class=""icon-white {0}""></i>{1}", _iconCss, _caption);
				}
				else
				{
					innerPart = string.Format(@"<i class=""{0}""></i>{1}", _iconCss, _caption);
				}

			a.Attributes.Add("href", new UrlHelper(HttpContext.Current.Request.RequestContext).Action(_action, _controller, _routeDictionary));
			a.InnerHtml = innerPart;

			return a.ToString();
		}

		private bool IsPathActive()
		{
			var routeData = HttpContext.Current.Request.RequestContext.RouteData;

			var currentAction = (string)(routeData.DataTokens["controller"] ?? routeData.GetRequiredString("action"));
			var currentController = (string)(routeData.DataTokens["controller"] ?? routeData.GetRequiredString("controller"));
			var currentArea = routeData.DataTokens["area"] as string;

			switch (_showActiveLevel)
			{
				case ShowActiveLevels.ControllerLevel:
					if (string.Equals(currentController, _controller, StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
					break;

				case ShowActiveLevels.ControllerAndActionLevel:
					if (string.Equals(currentAction, _action, StringComparison.InvariantCultureIgnoreCase) &&
						string.Equals(currentController, _controller, StringComparison.InvariantCultureIgnoreCase) &&
						string.Equals(currentArea, _area, StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
					break;

				case ShowActiveLevels.ActionLevel:
					if (string.Equals(currentAction, _action, StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
					break;

				case ShowActiveLevels.AreaLevel:
					if (!string.IsNullOrWhiteSpace(_area) && string.Equals(currentArea, _area, StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
					break;

				default:
					return false;
			}


			return false;
		}

		#endregion
	}
}
