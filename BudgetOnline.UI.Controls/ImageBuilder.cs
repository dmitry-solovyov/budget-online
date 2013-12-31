
using System.Web;

namespace BudgetOnline.UI.Controls
{
	public class ImageBuilder : IBuilder
	{
		protected UIBuilder _builder;

		protected string _imageUrl;
		protected string _title;
		protected string _caption;
		protected string _class;
		protected string _alt;

		public static ImageBuilder Create()
		{
			return new ImageBuilder();
		}

		public virtual ImageBuilder Caption(string text)
		{
			_caption = text;

			return this;
		}

		public virtual ImageBuilder Css(string @class)
		{
			_class = @class;

			return this;
		}

		public virtual ImageBuilder Src(string url, string title)
		{
			_imageUrl = url;
			_title = title;

			return this;
		}

		public virtual ImageBuilder Alt(string alt)
		{
			_alt = alt;

			return this;
		}


		public virtual HtmlString Build()
		{
			_builder = new UIBuilder();
			_builder.CollapseEmptyTags(true);

			_builder
				.Tag("img")
				.Css(_class)
				.Attr("src", _imageUrl)
				.Attr("title", _title)
				.Attr("alt", _alt);

			return _builder.Build();
		}
	}
}
