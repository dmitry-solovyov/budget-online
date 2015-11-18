using System.Collections.Generic;
using System.Web;

namespace BudgetOnline.UI.Controls.Buttons
{
    public class ButtonBuilder : IBuilder
    {
        protected UIBuilder _builder;

        private ButtonTypes _buttonType = ButtonTypes.Button;
        private Dictionary<string, string> _attributes = new Dictionary<string, string>();

        public static ButtonBuilder Get()
        {
            return new ButtonBuilder();
        }

        private string _id;
        public virtual ButtonBuilder Id(string id)
        {
            _id = id;

            return this;
        }

        public virtual ButtonBuilder Attr(string key, string value)
        {
            _attributes.Add(key, value);

            return this;
        }

        private string _caption;
        public virtual ButtonBuilder Caption(string text)
        {
            _caption = text;

            return this;
        }

        private string _class;
        public virtual ButtonBuilder Css(string @class)
        {
            _class = @class;

            return this;
        }

        private string _clientClickAction;
        public virtual ButtonBuilder ClientClickAction(string clientClickAction)
        {
            _clientClickAction = clientClickAction;

            return this;
        }

        private string _tooltip;
        private string _iconCss;
        public virtual ButtonBuilder Icon(string css, string tooltip)
        {
            _iconCss = css;
            _tooltip = tooltip;

            return this;
        }
        public virtual ButtonBuilder Icon(string css)
        {
            _iconCss = css;
            _tooltip = string.Empty;

            return this;
        }

        protected ButtonBuilder ButtonType(ButtonTypes buttonType)
        {
            _buttonType = buttonType;

            return this;
        }

        private string _redirectTo;
        public virtual ButtonBuilder RedirectTo(string url)
        {
            _redirectTo = url;

            return this;
        }


        public virtual HtmlString Build()
        {
            _builder = new UIBuilder().CollapseEmptyTags(false);


            if (!string.IsNullOrWhiteSpace(_clientClickAction))
            {
                _builder.Tag("a").Css(("btn " + _class).Trim()).Attr("href", "#").Attr("onclick", string.Format("{0}(this); return false;", _clientClickAction));

                if (!string.IsNullOrWhiteSpace(_iconCss))
                {
                    _builder.Child(() => new UIBuilder().Tag("i").Css(_iconCss).Attr("tooltip", _tooltip).CollapseEmptyTags(false));
                    //_builder.Child(() => new UIBuilder().Tag("span").Css("caption").Caption(_caption));
                }
                else
                    _builder.Content(new HtmlString(_caption));
            }
            else if (!string.IsNullOrWhiteSpace(_redirectTo))
            {
                if (string.IsNullOrWhiteSpace(_iconCss))
                {
                    _builder.Tag("a").Css(_class).Attr("href", _redirectTo).Content(_caption);
                }
                else
                {
                    var innerContent = string.Format(@"<i class=""{0}""></i>{1}", _iconCss, _caption);
                    _builder.Tag("a").Css(_class).Attr("href", _redirectTo).Content(innerContent);
                }
            }
            else
            {
                _builder.Tag("button").Css(_class);

                if (_buttonType != ButtonTypes.Button)
                    _builder.Attr("type", _buttonType.ToString().ToLower());

                if (!string.IsNullOrWhiteSpace(_iconCss))
                {
                    //_builder.Child(() => new ImageBuilder().Css("icon").Src(_iconCss, _tooltip));
                    _builder.Child(() => new UIBuilder().Tag("i").Css(_iconCss).Attr("tooltip", _tooltip));
                    //_builder.Child(() => new UIBuilder().Tag("span").Css("caption").Caption(_caption));
                }
                else
                    _builder.Content(new HtmlString(_caption));
            }

            if (!string.IsNullOrWhiteSpace(_id))
                _builder.Id(_id);

            if (_attributes.Count > 0)
                foreach (var attribute in _attributes)
                    _builder.Attr(attribute.Key, attribute.Value);

            return _builder.Build();
        }

        public enum ButtonTypes
        {
            Reset,
            Submit,
            Button
        }
    }
}
