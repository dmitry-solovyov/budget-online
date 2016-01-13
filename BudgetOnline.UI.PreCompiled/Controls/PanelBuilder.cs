using System;
using System.Web;
using System.Web.WebPages;
using ASP;
using BudgetOnline.UI.PreCompiled.Models;

namespace BudgetOnline.UI.PreCompiled.Controls
{
    public class PanelBuilder : IBuilder
    {
        public PanelBuilder()
        {
            _suppressHeaderIfEmpty = true;
            _suppressFooterIfEmpty = true;
            _suppressContentIfEmpty = false;
        }

        public static PanelBuilder Create()
        {
            return new PanelBuilder();
        }

        protected ContainerBuilder HeaderBuilder = new ContainerBuilder().CollapseEmptyTags(true).HideIfEmpty(true);
        protected ContainerBuilder ContentBuilder = new ContainerBuilder().CollapseEmptyTags(true);
        protected ContainerBuilder FooterBuilder = new ContainerBuilder().CollapseEmptyTags(true);

        private bool _suppressHeaderIfEmpty;
        private bool _suppressFooterIfEmpty;
        private bool _suppressContentIfEmpty;

        public PanelBuilder SuppressFooterIfEmpty(bool isSupress)
        {
            _suppressFooterIfEmpty = isSupress;
            return this;
        }

        public PanelBuilder SuppressHeaderIfEmpty(bool isSupress)
        {
            _suppressHeaderIfEmpty = isSupress;
            return this;
        }

        public PanelBuilder SuppressContentIfEmpty(bool isSupress)
        {
            _suppressContentIfEmpty = isSupress;
            return this;
        }

        #region Header

        public PanelBuilder Header(string html)
        {
            HeaderBuilder.Content(html);
            return this;
        }

        public PanelBuilder Header(Func<string> builder)
        {
            HeaderBuilder.Content(builder);
            return this;
        }

        public PanelBuilder Header(Func<HtmlString> builder)
        {
            if (builder != null)
                HeaderBuilder.Content(builder);
            return this;
        }

        public PanelBuilder Header(IBuilder builder)
        {
            HeaderBuilder.Content(builder);
            return this;
        }

        #endregion


        #region Footer

        public PanelBuilder Footer(string html)
        {
            FooterBuilder.Content(html);
            return this;
        }

        public PanelBuilder Footer(Func<string> builder)
        {
            if (builder != null)
                FooterBuilder.Content(builder);
            return this;
        }

        public PanelBuilder Footer(Func<HtmlString> builder)
        {
            FooterBuilder.Content(builder);
            return this;
        }

        public PanelBuilder Footer(IBuilder builder)
        {
            FooterBuilder.Content(builder);
            return this;
        }

        #endregion


        #region Content

        public PanelBuilder Content(string html)
        {
            ContentBuilder.Content(html);
            return this;
        }

        public PanelBuilder Content(HtmlString html)
        {
            ContentBuilder.Content(html);
            return this;
        }

        public PanelBuilder Content(Func<string> builder)
        {
            ContentBuilder.Content(builder);
            return this;
        }

        public PanelBuilder Content(Func<object, HelperResult> result)
        {
            ContentBuilder.Content(result);
            return this;
        }

        public PanelBuilder Content(Func<HtmlString> builder)
        {
            ContentBuilder.Content(builder);
            return this;
        }

        public PanelBuilder Content(IBuilder builder)
        {
            ContentBuilder.Content(builder);
            return this;
        }

        private string _contentRefreshUrl;
        public PanelBuilder ContentRefreshUrl(string url)
        {
            _contentRefreshUrl = url;
            return this;
        }

        private string _contentRefreshCallbackFunction;
        public PanelBuilder ContentRefreshCallbackFunction(string functionName)
        {
            _contentRefreshCallbackFunction = functionName;
            return this;
        }

        #endregion

        public HtmlString Build()
        {
            var model = new DynamicContainerModel
                {
                    RequestUrl = _contentRefreshUrl,
                    CallbackClientFunction = _contentRefreshCallbackFunction,
                    Caption = HeaderBuilder.IsEmpty() && _suppressHeaderIfEmpty ? new HtmlString("") : HeaderBuilder.Build(),
                    Content = ContentBuilder.IsEmpty() ? null : ContentBuilder.Build(),
                };

            if (model.Content != null)
                model.IsAutoload = false;

            var container = new _Page_Views_DynamicContainer_cshtml();
            return new HtmlString(container.Render(model).ToHtmlString());
        }
    }
}
