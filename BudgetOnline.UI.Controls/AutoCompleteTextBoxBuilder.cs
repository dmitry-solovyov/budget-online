
using System.Collections.Generic;
using System.Web;
using BudgetOnline.UI.Models;

namespace BudgetOnline.UI.Controls
{
	public class AutoCompleteTextBoxBuilder : IBuilder
	{
		protected UIBuilder _builder;

		protected string _id;
		protected string _class;
		protected string _requestPath;
		private Dictionary<string, string> _attributes = new Dictionary<string, string>();

		public virtual AutoCompleteTextBoxBuilder Id(string id)
		{
			_id = id;

			return this;
		}

		public virtual AutoCompleteTextBoxBuilder Attr(string key, string value)
		{
			_attributes.Add(key, value);

			return this;
		}

		public virtual AutoCompleteTextBoxBuilder Css(string @class)
		{
			_class = @class;

			return this;
		}

		protected string _iconCss;
		public virtual AutoCompleteTextBoxBuilder Icon(string css)
		{
			_iconCss = css;

			return this;
		}

		protected string _value;
		public virtual AutoCompleteTextBoxBuilder Value(string value)
		{
			_value = value;

			return this;
		}

		protected string _name;
		public virtual AutoCompleteTextBoxBuilder Name(string name)
		{
			_name = name;

			return this;
		}

		public virtual AutoCompleteTextBoxBuilder RequestPath(string path)
		{
			_requestPath = path;

			return this;
		}


		public virtual HtmlString Build()
		{
			var model = new AutoCompleteTextBoxModel
			            	{
			            		RequestUrl = _requestPath,
								Value = _value,
								Name = _name,
			            	};

			var control = new AutoCompleteTextBox();
			return new HtmlString(control.Render(model).ToHtmlString());
		}
	}
}
