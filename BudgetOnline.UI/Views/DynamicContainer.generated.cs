﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.2034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BudgetOnline.UI.Views
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 1 "..\..\Views\DynamicContainer.cshtml"
    using BudgetOnline.UI.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DynamicContainer.cshtml")]
    public partial class DynamicContainer : System.Web.Mvc.WebViewPage<dynamic>
    {
        
        #line 2 "..\..\Views\DynamicContainer.cshtml"


        #line default
        #line hidden

public System.Web.WebPages.HelperResult Render(DynamicContainerModel model)
    {
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 5 "..\..\Views\DynamicContainer.cshtml"
     

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"\">\r\n        <div id=\"");



#line 7 "..\..\Views\DynamicContainer.cshtml"
WriteTo(@__razor_helper_writer, model.UniqeId);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\" class=\"\" data-name=\"");



#line 7 "..\..\Views\DynamicContainer.cshtml"
                         WriteTo(@__razor_helper_writer, model.Name);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\" data-dynamic-url=\'");



#line 7 "..\..\Views\DynamicContainer.cshtml"
                                                          WriteTo(@__razor_helper_writer, model.RequestUrl);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\' data-autoload=\'");



#line 7 "..\..\Views\DynamicContainer.cshtml"
                                                                                              WriteTo(@__razor_helper_writer, model.IsAutoload.ToString().ToLower());

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\' \r\n             data-autoload-delay=\'");



#line 8 "..\..\Views\DynamicContainer.cshtml"
    WriteTo(@__razor_helper_writer, model.IsAutoload && model.AutoloadDelay > 0 ? model.AutoloadDelay.ToString() : "0");

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\'>\r\n            \r\n");



#line 10 "..\..\Views\DynamicContainer.cshtml"
             if (!string.IsNullOrWhiteSpace(model.Caption.ToHtmlString()))
            {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                <div class=\"row darkback border-bt\">\r\n                    <div cl" +
"ass=\"col-md-10\">\r\n                        <strong>");



#line 14 "..\..\Views\DynamicContainer.cshtml"
  WriteTo(@__razor_helper_writer, model.Caption);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, @"</strong>
                    </div>
                    <div class=""col-md-2"">
                        <a class=""pull-right btn btn-xs"" href=""#""><i class=""glyphicon glyphicon-refresh""></i>
                        </a>
                    </div>
                </div>
");



#line 21 "..\..\Views\DynamicContainer.cshtml"
            }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "            <div class=\"row\" style=\"padding: 3px\">\r\n");



#line 23 "..\..\Views\DynamicContainer.cshtml"
                 if (model.Content != null && !string.IsNullOrWhiteSpace(model.Content.ToHtmlString()))
                {
                    
#line default
#line hidden


#line 25 "..\..\Views\DynamicContainer.cshtml"
WriteTo(@__razor_helper_writer, new HtmlString(model.Content.ToHtmlString()));

#line default
#line hidden


#line 25 "..\..\Views\DynamicContainer.cshtml"
                                                                   
                }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "            </div>\r\n            <div class=\"row darkback poor-text\">\r\n           " +
"     <span class=\"pull-right small\">\r\n                    ");



#line 30 "..\..\Views\DynamicContainer.cshtml"
WriteTo(@__razor_helper_writer, DateTime.Now);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\r\n                </span>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");



#line 35 "..\..\Views\DynamicContainer.cshtml"

#line default
#line hidden

});

}


        public DynamicContainer()
        {
        }
        public override void Execute()
        {


WriteLiteral("\r\n");




        }
    }
}
#pragma warning restore 1591
