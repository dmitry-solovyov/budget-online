using System.Web;

namespace BudgetOnline.UI.Models
{
	public class DynamicContainerModel : UIControlModel
	{
		public bool IsAutoload { get; set; }
		/// <summary>
		/// Delay in milliseconds
		/// </summary>
		public int AutoloadDelay { get; set; }
		public string RequestUrl { get; set; }
		public string CallbackClientFunction { get; set; }
		public HtmlString Caption { get; set; }
		public string IconCss { get; set; }
		public bool IsShowRefreshTime { get; set; }

		public HtmlString Header { get; set; }
		public HtmlString Content { get; set; }
		public HtmlString Footer { get; set; }

		public DynamicContainerModel()
		{
			IsAutoload = true;
			AutoloadDelay = 671;
		}
	}
}