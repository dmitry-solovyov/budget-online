﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1008
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BudgetOnline.Web.Views
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/_AnonimousPage.cshtml")]
    public partial class AnonimousPage : System.Web.Mvc.WebViewPage<dynamic>
    {
        public AnonimousPage()
        {
        }
        public override void Execute()
        {
WriteLiteral("<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta charset=\"utf-8\" lang=\"en\"></meta>\r\n   " +
" <title>");


            
            #line 5 "..\..\Views\_AnonimousPage.cshtml"
      Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("</title>\r\n    <link href=\"");


            
            #line 6 "..\..\Views\_AnonimousPage.cshtml"
           Write(Url.Content("~/Content/site.css"));

            
            #line default
            #line hidden
WriteLiteral("\" rel=\"stylesheet\" type=\"text/css\" />\r\n    <link href=\"");


            
            #line 7 "..\..\Views\_AnonimousPage.cshtml"
           Write(Url.Content("~/Content/bootstrap.min.css"));

            
            #line default
            #line hidden
WriteLiteral("\" rel=\"stylesheet\" type=\"text/css\" />\r\n    ");



WriteLiteral("\r\n    <style type=\"text/css\">\r\n        body\r\n        {\r\n            padding-top: " +
"40px;\r\n            padding-bottom: 40px;\r\n            background-color: #f5f5f5;" +
"\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    ");


            
            #line 19 "..\..\Views\_AnonimousPage.cshtml"
Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n    <script src=\"");


            
            #line 20 "..\..\Views\_AnonimousPage.cshtml"
            Write(Url.Content("~/Scripts/jquery-2.0.3.min.js"));

            
            #line default
            #line hidden
WriteLiteral("\" type=\"text/javascript\"></script>\r\n    <script src=\"");


            
            #line 21 "..\..\Views\_AnonimousPage.cshtml"
            Write(Url.Content("~/Scripts/modernizr-2.6.2.js"));

            
            #line default
            #line hidden
WriteLiteral("\" type=\"text/javascript\"></script>\r\n    <script src=\"");


            
            #line 22 "..\..\Views\_AnonimousPage.cshtml"
            Write(Url.Content("~/Scripts/bootstrap.min.js"));

            
            #line default
            #line hidden
WriteLiteral("\" type=\"text/javascript\"></script>\r\n</body>\r\n</html>\r\n");


        }
    }
}
#pragma warning restore 1591
