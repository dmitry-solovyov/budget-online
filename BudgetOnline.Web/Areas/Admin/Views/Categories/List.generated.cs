﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
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
    
    #line 1 "..\..\Areas\Admin\Views\Categories\List.cshtml"
    using BudgetOnline.Web.Areas.Admin.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Categories/List.cshtml")]
    public partial class _Areas_Admin_Views_Categories_List_cshtml : System.Web.Mvc.WebViewPage<IEnumerable<CategoryListViewModel>>
    {
        public _Areas_Admin_Views_Categories_List_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Areas\Admin\Views\Categories\List.cshtml"
  
    ViewBag.Title = "Категории";
    Layout = "~/Areas/Admin/Views/Shared/_AdminLayoutPage.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"btn-toolbar\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"btn-group\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"btn\"");

WriteAttribute("href", Tuple.Create(" href=\"", 274), Tuple.Create("\"", 302)
            
            #line 9 "..\..\Areas\Admin\Views\Categories\List.cshtml"
, Tuple.Create(Tuple.Create("", 281), Tuple.Create<System.Object, System.Int32>(Url.Action("create")
            
            #line default
            #line hidden
, 281), false)
);

WriteLiteral("><i");

WriteLiteral(" class=\"glyphicon-plus\"");

WriteLiteral("></i>Новая запись</a>\r\n    </div>\r\n</div>\r\n");

            
            #line 12 "..\..\Areas\Admin\Views\Categories\List.cshtml"
Write(Html.BudgetOnlineWeb()
    .Table<CategoryListViewModel>()
    .Css("table table-striped")
    .Rows(Model)
    .Columns(columns =>
                {
                    columns.Icon().IconCss(m => m.IsDisabled ? "glyphicon-lock" : (m.IsDefault ? "glyphicon-star" : "")).Span(1);
                    columns.Bound(m => m.Name).Span(7);
                    columns.Commands().Cell(m => m.Commands).Span(2);
                })
    .Build()
    );

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591