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
    
    #line 6 "..\..\Views\Paginator.cshtml"
    using BudgetOnline.UI.Extensions;
    
    #line default
    #line hidden
    
    #line 7 "..\..\Views\Paginator.cshtml"
    using BudgetOnline.UI.Helpers;
    
    #line default
    #line hidden
    
    #line 8 "..\..\Views\Paginator.cshtml"
    using BudgetOnline.UI.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class Paginator : System.Web.WebPages.HelperPage
    {

#line 10 "..\..\Views\Paginator.cshtml"
public static System.Web.WebPages.HelperResult Render(PaginationModel model)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 11 "..\..\Views\Paginator.cshtml"
 
    var pages = model.GetPages();
    var nextPageSize = model.PageSizes.FirstOrDefault(p => p > model.PageSize);
    if (nextPageSize == 0)
    {
        nextPageSize = model.PageSizes[0];
    }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"visible-xs clearfix\"");

WriteLiteralTo(__razor_helper_writer, " style=\"height: 10px\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n    </div>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"row-fluid\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n        <div");

WriteLiteralTo(__razor_helper_writer, " class=\"col-md-5 col-md-offset-4 col-sm-4 col-sm-offset-2\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n            <ul");

WriteLiteralTo(__razor_helper_writer, " class=\"pagination pagination-sm pull-left\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                <li><a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 669), Tuple.Create("\"", 726)

#line 23 "..\..\Views\Paginator.cshtml"
, Tuple.Create(Tuple.Create("", 676), Tuple.Create<System.Object, System.Int32>(UIHelper.GetUrlQithNewQueryParameter("page", "1")

#line default
#line hidden
, 676), false)
);

WriteLiteralTo(__razor_helper_writer, ">«</a></li>\r\n");


#line 24 "..\..\Views\Paginator.cshtml"
                

#line default
#line hidden

#line 24 "..\..\Views\Paginator.cshtml"
                 foreach (var page in pages)
                {
                    if (model.Page == page)
                    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                    <li");

WriteLiteralTo(__razor_helper_writer, " class=\"active\"");

WriteLiteralTo(__razor_helper_writer, "><a");

WriteLiteralTo(__razor_helper_writer, " href=\"#\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 28 "..\..\Views\Paginator.cshtml"
                     WriteTo(__razor_helper_writer, page);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</a></li>\r\n");


#line 29 "..\..\Views\Paginator.cshtml"
                    }
                    else
                    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                    <li><a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 1038), Tuple.Create("\"", 1108)

#line 32 "..\..\Views\Paginator.cshtml"
, Tuple.Create(Tuple.Create("", 1045), Tuple.Create<System.Object, System.Int32>(UIHelper.GetUrlQithNewQueryParameter("page", @page.ToString())

#line default
#line hidden
, 1045), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 32 "..\..\Views\Paginator.cshtml"
                                                                    WriteTo(__razor_helper_writer, page);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</a></li>\r\n");


#line 33 "..\..\Views\Paginator.cshtml"
                    }
                }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                <li><a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 1190), Tuple.Create("\"", 1271)

#line 35 "..\..\Views\Paginator.cshtml"
, Tuple.Create(Tuple.Create("", 1197), Tuple.Create<System.Object, System.Int32>(UIHelper.GetUrlQithNewQueryParameter("page", model.PagesCount.ToString())

#line default
#line hidden
, 1197), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n                    »</a></li>\r\n            </ul>\r\n        </div>\r\n        <di" +
"v");

WriteLiteralTo(__razor_helper_writer, " class=\"col-md-3 col-sm-6\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n            <div");

WriteLiteralTo(__razor_helper_writer, " style=\"margin-top: 10px\"");

WriteLiteralTo(__razor_helper_writer, " class=\"pull-right\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                <label");

WriteLiteralTo(__razor_helper_writer, " class=\"\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                    Страница:\r\n                </label>\r\n                <div");

WriteLiteralTo(__razor_helper_writer, " class=\"btn-group\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                    <button");

WriteLiteralTo(__razor_helper_writer, " class=\"btn btn-default btn-xs dropdown-toggle\"");

WriteLiteralTo(__razor_helper_writer, " data-toggle=\"dropdown\"");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 1675), Tuple.Create("\"", 1756)

#line 45 "..\..\Views\Paginator.cshtml"
                        , Tuple.Create(Tuple.Create("", 1682), Tuple.Create<System.Object, System.Int32>(UIHelper.GetUrlQithNewQueryParameter("pageSize", nextPageSize.ToString())

#line default
#line hidden
, 1682), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "                        ");


#line 46 "..\..\Views\Paginator.cshtml"
WriteTo(__razor_helper_writer, model.PageSize);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "&nbsp;&nbsp;&nbsp; <span");

WriteLiteralTo(__razor_helper_writer, " class=\"caret\"");

WriteLiteralTo(__razor_helper_writer, "></span>\r\n                    </button>\r\n                    <ul");

WriteLiteralTo(__razor_helper_writer, " class=\"dropdown-menu\"");

WriteLiteralTo(__razor_helper_writer, " id=\"setPageSize\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");


#line 49 "..\..\Views\Paginator.cshtml"
                        

#line default
#line hidden

#line 49 "..\..\Views\Paginator.cshtml"
                         foreach (var pageSize in model.PageSizes)
                        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                            <li><a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 2074), Tuple.Create("\"", 2154)

#line 51 "..\..\Views\Paginator.cshtml"
, Tuple.Create(Tuple.Create("", 2081), Tuple.Create<System.Object, System.Int32>(UIHelper.GetUrlQithNewQueryParameter("pageSize", pageSize.ToString("D"))

#line default
#line hidden
, 2081), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 51 "..\..\Views\Paginator.cshtml"
                                                                                      WriteTo(__razor_helper_writer, pageSize);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</a></li>\r\n");


#line 52 "..\..\Views\Paginator.cshtml"
                        }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                    </ul>\r\n                </div>\r\n            </div>\r\n        </" +
"div>\r\n    </div>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"clearfix\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n    </div>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"\"");

WriteLiteralTo(__razor_helper_writer, " style=\"height: 10px\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n    </div>\r\n");


#line 62 "..\..\Views\Paginator.cshtml"


#line default
#line hidden
});

#line 62 "..\..\Views\Paginator.cshtml"
}
#line default
#line hidden

    }
}
#pragma warning restore 1591