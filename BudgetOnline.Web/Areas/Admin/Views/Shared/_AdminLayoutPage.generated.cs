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
    
    #line 6 "..\..\Areas\Admin\Views\Shared\_AdminLayoutPage.cshtml"
    using BudgetOnline.Web.UI.Controls;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Shared/_AdminLayoutPage.cshtml")]
    public partial class _Areas_Admin_Views_Shared__AdminLayoutPage_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Areas_Admin_Views_Shared__AdminLayoutPage_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Areas\Admin\Views\Shared\_AdminLayoutPage.cshtml"
  
    Layout = "../../../../Views/Shared/_LayoutMain.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">\r\n");

            
            #line 6 "..\..\Areas\Admin\Views\Shared\_AdminLayoutPage.cshtml"
        
            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 7 "..\..\Areas\Admin\Views\Shared\_AdminLayoutPage.cshtml"
    Write(Html.BudgetOnlineWeb().Menu(MenuTypes.ListGroup)
            .ShowActiveLevel(ShowActiveLevels.ControllerLevel)
            .MenuItems(new[]
                        {
                            new MenuItemBuilder().Caption("Пользователи").Action("list").Controller("users")
                                .Area("admin").IconCss("glyphicon-home"),
							
                            new MenuItemBuilder().Caption("Счета").Action("list").Controller("accounts")
                                .Area("admin").IconCss("glyphicon-list"),
							
                            new MenuItemBuilder().Caption("Категории").Action("list").Controller("categories")
                                .Area("admin").IconCss("glyphicon-list"),
							
                                new MenuItemBuilder().Caption("Валюты").Action("list").Controller("currencies")
                                .Area("admin").IconCss("glyphicon-home"),
							
                            new MenuItemBuilder().Caption("Курсы валют").Action("list").Controller("currencyRates")
                                .Area("admin").IconCss("glyphicon-home"),
							
                            new MenuItemBuilder().Caption("Метки").Action("list").Controller("tags")
                                .Area("admin").IconCss("glyphicon-book"),
							
                            new MenuItemBuilder().Caption("Периоды").Action("list").Controller("periodTypes")
                                .Area("admin"),
							
                            new MenuItemBuilder().Caption("Сообщения").Action("list").Controller("messages")
                                .Area("admin"),
                        })
            .ActiveLinkCss("active")
            .Build());

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"col-md-9\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 39 "..\..\Areas\Admin\Views\Shared\_AdminLayoutPage.cshtml"
   Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n</div>\r\n");

        }
    }
}
#pragma warning restore 1591