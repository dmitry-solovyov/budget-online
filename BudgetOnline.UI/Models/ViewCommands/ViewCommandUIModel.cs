using System.Collections.Generic;

namespace BudgetOnline.UI.Models.ViewCommands
{
	public class ViewCommandUIModel
	{
		public bool IsDefault { get; set; }
		public bool IsVisible { get; set; }
		public bool IsDisabled { get; set; }
		public string IconCssClass { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public ViewCommandModel Command { get; set; }

		public IEnumerable<ViewCommandUIModel> ChildCommands { get; set; }

		public bool IsDividerBefore { get; set; }
		public bool IsDividerAfter { get; set; }

		public ViewCommandUIModel()
		{
			IsVisible = true;
		}
	}
}
