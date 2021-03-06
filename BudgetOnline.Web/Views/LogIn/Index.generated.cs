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
    
    #line 1 "..\..\Views\LogIn\Index.cshtml"
    using BudgetOnline.Web.Infrastructure.Controls;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/LogIn/Index.cshtml")]
    public partial class _Views_LogIn_Index_cshtml : System.Web.Mvc.WebViewPage<BudgetOnline.Web.ViewModels.Security.LogInViewModel>
    {
        public _Views_LogIn_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\LogIn\Index.cshtml"
  
    ViewBag.Title = "Log In";
    Layout = "~/Views/_AnonimousPage.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"container\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 8 "..\..\Views\LogIn\Index.cshtml"
Write(Html.BudgetOnlineWeb().Form()
        .Css("form-signin")
        .Header("Введите логин")
        .HeaderCss("form-signin-heading")
        .NoRowHeader()
        .Content(
            item => new System.Web.WebPages.HelperResult(__razor_template_writer => {

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n");

WriteTo(__razor_template_writer, 
            
            #line 15 "..\..\Views\LogIn\Index.cshtml"
    
            
            #line default
            #line hidden
            
            #line 15 "..\..\Views\LogIn\Index.cshtml"
     Html.AntiForgeryToken());

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n    <div");

WriteLiteralTo(__razor_template_writer, " class=\"form-group\"");

WriteLiteralTo(__razor_template_writer, " id=\"\"");

WriteLiteralTo(__razor_template_writer, ">\r\n        <div");

WriteLiteralTo(__razor_template_writer, " class=\"col-md-12\"");

WriteLiteralTo(__razor_template_writer, ">\r\n            <input");

WriteLiteralTo(__razor_template_writer, " class=\"form-control\"");

WriteLiteralTo(__razor_template_writer, " data-val=\"true\"");

WriteLiteralTo(__razor_template_writer, " data-val-required=\"The UserName field is required.\"");

WriteLiteralTo(__razor_template_writer, "\r\n                id=\"UserName\"");

WriteLiteralTo(__razor_template_writer, " name=\"UserName\"");

WriteLiteralTo(__razor_template_writer, " placeholder=\"Email\"");

WriteLiteralTo(__razor_template_writer, " type=\"text\"");

WriteLiteralTo(__razor_template_writer, " value=\"\"");

WriteLiteralTo(__razor_template_writer, " />\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteralTo(__razor_template_writer, " class=\"form-group\"");

WriteLiteralTo(__razor_template_writer, " id=\"\"");

WriteLiteralTo(__razor_template_writer, ">\r\n        <div");

WriteLiteralTo(__razor_template_writer, " class=\"col-md-12\"");

WriteLiteralTo(__razor_template_writer, ">\r\n            <input");

WriteLiteralTo(__razor_template_writer, " class=\"form-control\"");

WriteLiteralTo(__razor_template_writer, " data-val=\"true\"");

WriteLiteralTo(__razor_template_writer, " data-val-required=\"The Password field is required.\"");

WriteLiteralTo(__razor_template_writer, "\r\n                id=\"Password\"");

WriteLiteralTo(__razor_template_writer, " name=\"Password\"");

WriteLiteralTo(__razor_template_writer, " placeholder=\"Пароль\"");

WriteLiteralTo(__razor_template_writer, " type=\"password\"");

WriteLiteralTo(__razor_template_writer, " value=\"\"");

WriteLiteralTo(__razor_template_writer, " />\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteralTo(__razor_template_writer, " class=\"form-group\"");

WriteLiteralTo(__razor_template_writer, " id=\"\"");

WriteLiteralTo(__razor_template_writer, ">\r\n        <div");

WriteLiteralTo(__razor_template_writer, " class=\"col-md-12\"");

WriteLiteralTo(__razor_template_writer, ">\r\n            <label");

WriteLiteralTo(__razor_template_writer, " class=\"checkbox-inline\"");

WriteLiteralTo(__razor_template_writer, ">\r\n                <input");

WriteLiteralTo(__razor_template_writer, " data-val=\"true\"");

WriteLiteralTo(__razor_template_writer, " data-val-required=\"The Запомнить меня field is required.\"");

WriteLiteralTo(__razor_template_writer, "\r\n                    id=\"RememberMe\"");

WriteLiteralTo(__razor_template_writer, " name=\"RememberMe\"");

WriteLiteralTo(__razor_template_writer, " type=\"checkbox\"");

WriteLiteralTo(__razor_template_writer, " value=\"true\"");

WriteLiteralTo(__razor_template_writer, " />\r\n                <input");

WriteLiteralTo(__razor_template_writer, " name=\"RememberMe\"");

WriteLiteralTo(__razor_template_writer, " type=\"hidden\"");

WriteLiteralTo(__razor_template_writer, " value=\"false\"");

WriteLiteralTo(__razor_template_writer, " />\r\n                Запомнить меня\r\n            </label>\r\n        </div>\r\n    </" +
"div>\r\n    ");

WriteLiteralTo(__razor_template_writer, "\r\n");

            
            #line 39 "..\..\Views\LogIn\Index.cshtml"
})        )
        .ActionsContent(
            item => new System.Web.WebPages.HelperResult(__razor_template_writer => {

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n");

WriteTo(__razor_template_writer, 
            
            #line 42 "..\..\Views\LogIn\Index.cshtml"
    
            
            #line default
            #line hidden
            
            #line 42 "..\..\Views\LogIn\Index.cshtml"
     Html.BudgetOnlineWeb().PrimaryButton().Caption("Войти").Build());

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n    ");

            
            #line 43 "..\..\Views\LogIn\Index.cshtml"
         }))
        .Build());

            
            #line default
            #line hidden
WriteLiteral("\r\n</div>\r\n");

        }
    }
}
#pragma warning restore 1591
