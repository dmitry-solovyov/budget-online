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
    
    #line 1 "..\..\Areas\Admin\Views\Currencies\Create.cshtml"
    using BudgetOnline.Web.UI.Controls;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Currencies/Create.cshtml")]
    public partial class _Areas_Admin_Views_Currencies_Create_cshtml : System.Web.Mvc.WebViewPage<BudgetOnline.Web.Areas.Admin.Models.CurrencyEditViewModel>
    {
        public _Areas_Admin_Views_Currencies_Create_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Areas\Admin\Views\Currencies\Create.cshtml"
  
	ViewBag.Title = "Добавление валюты";
	Layout = "~/Areas/Admin/Views/Shared/_AdminLayoutPage.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h4>Добавление валюты</h4>\r\n<div");

WriteLiteral(" class=\"container\"");

WriteLiteral(">\r\n");

WriteLiteral("\t");

            
            #line 11 "..\..\Areas\Admin\Views\Currencies\Create.cshtml"
Write(Html.BudgetOnlineWeb()
 .Form(BudgetOnlineWebControls.FormTypes.Horizontal).ActionUrl(Url.Action("create"))
	.Content(item => new System.Web.WebPages.HelperResult(__razor_template_writer => {

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n");

WriteTo(__razor_template_writer, 
            
            #line 14 "..\..\Areas\Admin\Views\Currencies\Create.cshtml"
	
            
            #line default
            #line hidden
            
            #line 14 "..\..\Areas\Admin\Views\Currencies\Create.cshtml"
  Html.ValidationSummary());

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n");

WriteTo(__razor_template_writer, 
            
            #line 15 "..\..\Areas\Admin\Views\Currencies\Create.cshtml"
	
            
            #line default
            #line hidden
            
            #line 15 "..\..\Areas\Admin\Views\Currencies\Create.cshtml"
  Html.EditorForModel("~/Views/Shared/EditorTemplates/Object.cshtml"));

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n\t<div");

WriteLiteralTo(__razor_template_writer, " class=\"form-actions\"");

WriteLiteralTo(__razor_template_writer, ">\r\n\t\t<p>\r\n");

WriteTo(__razor_template_writer, 
            
            #line 18 "..\..\Areas\Admin\Views\Currencies\Create.cshtml"
			
            
            #line default
            #line hidden
            
            #line 18 "..\..\Areas\Admin\Views\Currencies\Create.cshtml"
    Html.BudgetOnlineWeb().PrimaryButton().Caption("Сохранить").Build());

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n");

WriteTo(__razor_template_writer, 
            
            #line 19 "..\..\Areas\Admin\Views\Currencies\Create.cshtml"
			
            
            #line default
            #line hidden
            
            #line 19 "..\..\Areas\Admin\Views\Currencies\Create.cshtml"
    Html.BudgetOnlineWeb().ButtonNormal().Caption("Отмена").RedirectTo(Url.Action("list")).Build());

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n\t\t</p>\r\n\t</div>\r\n\t");

            
            #line 22 "..\..\Areas\Admin\Views\Currencies\Create.cshtml"
      }))
	.Build());

            
            #line default
            #line hidden
WriteLiteral("\r\n</div>");

        }
    }
}
#pragma warning restore 1591