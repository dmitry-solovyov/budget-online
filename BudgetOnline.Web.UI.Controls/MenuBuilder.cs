using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BudgetOnline.UI.Controls;

namespace BudgetOnline.Web.UI.Controls
{
	public class MenuBuilder : IBuilder
	{
		private MenuTypes _menuType = MenuTypes.Menu;
		public MenuBuilder(MenuTypes menuType)
		{
			_menuType = menuType;
		}

		private ShowActiveLevels _showActiveLevel = ShowActiveLevels.ControllerAndActionLevel;
		public MenuBuilder ShowActiveLevel(ShowActiveLevels level)
		{
			_showActiveLevel = level;
			return this;
		}

		private bool _showActive = true;
		public MenuBuilder ShowActive(bool showActive)
		{
			_showActive = showActive;
			return this;
		}

		private string _menuCss;
		public MenuBuilder MenuCss(string menuCss)
		{
			_menuCss = menuCss;
			return this;
		}




		private string _activelinkCss;
		public MenuBuilder ActiveLinkCss(string activelinkCss)
		{
			_activelinkCss = activelinkCss;
			return this;
		}

		private IEnumerable<MenuItemBuilder> _items;
		public MenuBuilder MenuItems(IEnumerable<MenuItemBuilder> items)
		{
			_items = items;
			return this;
		}


		#region Implementation of IBuilder

		public HtmlString Build()
		{
			if (_items == null)
				throw new Exception("Items are not configured");

			UpdatePropertiesInChildItems();

			var sb = new StringBuilder(_items.Count());
			foreach (var item in _items)
			{
				var itemsRender = item.Build().ToHtmlString();
				sb.AppendLine(itemsRender);
			}

			var bodyTemplate = GetBodyTemplate();

			var result = string.Format(
					bodyTemplate,
					sb.ToString(),
					string.IsNullOrWhiteSpace(_menuCss) ? "nav" : _menuCss);


			return new MvcHtmlString(result);
		}

		private string GetBodyTemplate()
		{
			switch (_menuType)
			{
				case MenuTypes.EmptyContainer:
					return
@"<ul class='{1}'>{0}</ul>";

				case MenuTypes.Menu:
					return
@"<div class=""navbar"">
<div class=""navbar-inner"">
	<ul class='{1}'>{0}</ul>
 </div>
</div>";
				case MenuTypes.NavPills:
					return
@"<ul class=""nav nav-pills {1}"">{0}</ul>";

				case MenuTypes.TabsTop:
					return
@"<ul class=""nav nav-tabs {1}"">{0}</ul>";
				
				default:
					return string.Empty;
			}
		}

		private void UpdatePropertiesInChildItems()
		{
			foreach (var item in _items)
			{
				item.ActiveLinkCss(_activelinkCss);
				item.ShowActiveLevel(_showActiveLevel);
			}
		}

		#endregion
	}

	public enum ShowActiveLevels
	{
		ControllerLevel,
		ControllerAndActionLevel,
		ActionLevel,
		AreaLevel,
	}

	public enum MenuTypes
	{
		EmptyContainer,
		Menu,
		NavPills,
		TabsTop,
	}
}
