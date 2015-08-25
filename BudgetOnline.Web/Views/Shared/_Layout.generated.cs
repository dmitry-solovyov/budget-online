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

namespace BudgetOnline.Web.Views.Shared
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
    
    #line 1 "..\..\Views\Shared\_Layout.cshtml"
    using BudgetOnline.UI.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/_Layout.cshtml")]
    public partial class Layout : System.Web.Mvc.WebViewPage<dynamic>
    {
        public Layout()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Shared\_Layout.cshtml"
  
    Layout = "_LayoutMain.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-md-9 central-column\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 7 "..\..\Views\Shared\_Layout.cshtml"
   Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-md-3 right-column\"");

WriteLiteral(">\r\n        ");

WriteLiteral("\r\n        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 13 "..\..\Views\Shared\_Layout.cshtml"
            Write(Html.BudgetOnlineWeb().Panel()
           .Header("Текущий баланс")
           .Content(Html.Action("CurrentBalanceByCurrencies", "TransactionStatistics"))
           .ContentRefreshUrl(Url.Action("CurrentBalanceByCurrencies", "TransactionStatistics")).Build());

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 21 "..\..\Views\Shared\_Layout.cshtml"
            Write(Html.BudgetOnlineWeb().Panel()
           .Header("Текущий баланс по счетам")
           .Content(Html.Action("LightTotalByAccounts", "TransactionStatistics"))
           .ContentRefreshUrl(Url.Action("LightTotalByAccounts", "TransactionStatistics")).Build());

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 29 "..\..\Views\Shared\_Layout.cshtml"
           Write(RenderSection("RightBar", false));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");

DefineSection("Header", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 35 "..\..\Views\Shared\_Layout.cshtml"
Write(RenderSection("Header", false));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

        }
    }
}
#pragma warning restore 1591
