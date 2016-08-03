﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
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
    
    #line 2 "..\..\Views\DynamicContainer.cshtml"
    using BudgetOnline.UI.PreCompiled.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class _Views_DynamicContainer_cshtml : System.Web.WebPages.HelperPage
    {
        
        #line 3 "..\..\Views\DynamicContainer.cshtml"
            

        #line default
        #line hidden

#line 5 "..\..\Views\DynamicContainer.cshtml"
public static System.Web.WebPages.HelperResult Render(DynamicContainerModel model)
    {
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 6 "..\..\Views\DynamicContainer.cshtml"
     


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <div");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 148), Tuple.Create("\"", 169)

#line 7 "..\..\Views\DynamicContainer.cshtml"
, Tuple.Create(Tuple.Create("", 153), Tuple.Create<System.Object, System.Int32>(model.UniqeId

#line default
#line hidden
, 153), false)
);

WriteLiteralTo(__razor_helper_writer, " class=\"dyn-box\"");

WriteLiteralTo(__razor_helper_writer, " data-name=\"");


#line 7 "..\..\Views\DynamicContainer.cshtml"
                             WriteTo(__razor_helper_writer, model.Name);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\"");

WriteLiteralTo(__razor_helper_writer, " data-dynamic-url=\"");


#line 7 "..\..\Views\DynamicContainer.cshtml"
                                                              WriteTo(__razor_helper_writer, model.RequestUrl);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\"");

WriteLiteralTo(__razor_helper_writer, " \r\n         data-autoload=\"");


#line 8 "..\..\Views\DynamicContainer.cshtml"
WriteTo(__razor_helper_writer, model.IsAutoload.ToString().ToLower());


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\"");

WriteLiteralTo(__razor_helper_writer, " data-autoload-delay=\"");


#line 8 "..\..\Views\DynamicContainer.cshtml"
                                                          WriteTo(__razor_helper_writer, model.IsAutoload && model.AutoloadDelay > 0 ? model.AutoloadDelay.ToString() : "0");


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");


#line 9 "..\..\Views\DynamicContainer.cshtml"
        

#line default
#line hidden

#line 9 "..\..\Views\DynamicContainer.cshtml"
         if (!string.IsNullOrWhiteSpace(model.Caption.ToHtmlString()))
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <div");

WriteLiteralTo(__razor_helper_writer, " class=\"darkback compact header\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                <div");

WriteLiteralTo(__razor_helper_writer, " class=\"row\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"col-md-10\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                        <strong");

WriteLiteralTo(__razor_helper_writer, " class=\"\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 14 "..\..\Views\DynamicContainer.cshtml"
            WriteTo(__razor_helper_writer, model.Caption);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</strong>\r\n                    </div>\r\n                    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"col-md-2\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                        <a");

WriteLiteralTo(__razor_helper_writer, " class=\"pull-right btn btn-xs refresh\"");

WriteLiteralTo(__razor_helper_writer, " href=\"#\"");

WriteLiteralTo(__razor_helper_writer, "><i");

WriteLiteralTo(__razor_helper_writer, " class=\"glyphicon glyphicon-refresh\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                        </i></a>\r\n                    </div>\r\n                " +
"</div>\r\n            </div>\r\n");


#line 22 "..\..\Views\DynamicContainer.cshtml"
        }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <div");

WriteLiteralTo(__razor_helper_writer, " class=\"content\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");


#line 24 "..\..\Views\DynamicContainer.cshtml"
            

#line default
#line hidden

#line 24 "..\..\Views\DynamicContainer.cshtml"
             if (model.Content != null && !string.IsNullOrWhiteSpace(model.Content.ToHtmlString()))
            {
                

#line default
#line hidden

#line 26 "..\..\Views\DynamicContainer.cshtml"
WriteTo(__razor_helper_writer, new HtmlString(model.Content.ToHtmlString()));


#line default
#line hidden

#line 26 "..\..\Views\DynamicContainer.cshtml"
                                                               
            }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        </div>\r\n        <div");

WriteLiteralTo(__razor_helper_writer, " class=\"darkback poor-text compact footer\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n            <span");

WriteLiteralTo(__razor_helper_writer, " class=\"small pull-right\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "                ");


#line 31 "..\..\Views\DynamicContainer.cshtml"
WriteTo(__razor_helper_writer, DateTime.Now);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n            </span>\r\n        </div>\r\n    </div>\r\n");


#line 35 "..\..\Views\DynamicContainer.cshtml"


#line default
#line hidden
});

#line 35 "..\..\Views\DynamicContainer.cshtml"
}
#line default
#line hidden

    }
}
#pragma warning restore 1591
