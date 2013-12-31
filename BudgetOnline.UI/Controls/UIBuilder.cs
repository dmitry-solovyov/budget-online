using System.Globalization;
using System.Web;
using BudgetOnline.UI.Controls.Xml;

namespace BudgetOnline.UI.Controls
{
	public class UIBuilder : XmlNodeBuilder<UIBuilder>
	{
		public UIBuilder()
			: base(new XmlNodeBuilderSettings { CollapseEmptyTags = true })
		{
		}

		public UIBuilder CollapseEmptyTags(bool collapse)
		{
			XmlSettings.CollapseEmptyTags = collapse;
			return this;
		}

		public UIBuilder Caption(string caption)
		{
			Content(new HtmlString(caption));
			return this;
		}

		public UIBuilder Css(string @class)
		{
			Attr("class", @class);
			return this;
		}

		public UIBuilder Style(string style)
		{
			Attr("style", style);
			return this;
		}

		public UIBuilder Width(decimal width)
		{
			Attr("width", width.ToString(CultureInfo.InvariantCulture));
			return this;
		}

		public UIBuilder Height(decimal height)
		{
			Attr("height", height.ToString(CultureInfo.InvariantCulture));
			return this;
		}

		public bool IsEmpty()
		{
			return ContentExtractor == null 
				&& (ListOfAttributes == null || ListOfAttributes.Count == 0)
				&& (ListOfChilds == null || ListOfChilds.Count == 0);
		}
	}
}
