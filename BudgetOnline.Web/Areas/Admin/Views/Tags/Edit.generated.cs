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
    
    #line 1 "..\..\Areas\Admin\Views\Tags\Edit.cshtml"
    using BudgetOnline.Web.UI.Controls;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Tags/Edit.cshtml")]
    public partial class _Areas_Admin_Views_Tags_Edit_cshtml : System.Web.Mvc.WebViewPage<BudgetOnline.Web.Areas.Admin.Models.TagEditViewModel>
    {
        public _Areas_Admin_Views_Tags_Edit_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Areas\Admin\Views\Tags\Edit.cshtml"
  
    ViewBag.Title = "Редактирование метки";
    Layout = "~/Areas/Admin/Views/Shared/_AdminLayoutPage.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n<h4>\r\n    Редактирование</h4>\r\n<div");

WriteLiteral(" class=\"container\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 10 "..\..\Areas\Admin\Views\Tags\Edit.cshtml"
Write(Html.BudgetOnlineWeb()
    .Form(BudgetOnlineWebControls.FormTypes.Horizontal).ActionUrl(Url.Action("edit"))
    .Content(item => new System.Web.WebPages.HelperResult(__razor_template_writer => {

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n");

WriteTo(__razor_template_writer, 
            
            #line 13 "..\..\Areas\Admin\Views\Tags\Edit.cshtml"
    
            
            #line default
            #line hidden
            
            #line 13 "..\..\Areas\Admin\Views\Tags\Edit.cshtml"
     Html.ValidationSummary());

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n");

WriteTo(__razor_template_writer, 
            
            #line 14 "..\..\Areas\Admin\Views\Tags\Edit.cshtml"
    
            
            #line default
            #line hidden
            
            #line 14 "..\..\Areas\Admin\Views\Tags\Edit.cshtml"
     Html.EditorForModel("~/Views/Shared/EditorTemplates/Object.cshtml"));

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n    \r\n    <hr/>\r\n\r\n\t<div");

WriteLiteralTo(__razor_template_writer, " class=\"form-actions\"");

WriteLiteralTo(__razor_template_writer, ">\r\n        <p>\r\n");

WriteTo(__razor_template_writer, 
            
            #line 20 "..\..\Areas\Admin\Views\Tags\Edit.cshtml"
            
            
            #line default
            #line hidden
            
            #line 20 "..\..\Areas\Admin\Views\Tags\Edit.cshtml"
             Html.BudgetOnlineWeb().SubmitNormalButton(BudgetOnlineWebControls.ButtonCaptionTypes.Save).Build());

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n");

WriteTo(__razor_template_writer, 
            
            #line 21 "..\..\Areas\Admin\Views\Tags\Edit.cshtml"
            
            
            #line default
            #line hidden
            
            #line 21 "..\..\Areas\Admin\Views\Tags\Edit.cshtml"
             Html.BudgetOnlineWeb().ButtonNormal(BudgetOnlineWebControls.ButtonCaptionTypes.Cancel).RedirectTo(Url.Action("list")).Build());

            
            #line default
            #line hidden
WriteLiteralTo(__razor_template_writer, "\r\n        </p>\r\n    </div>\r\n    ");

            
            #line 24 "..\..\Areas\Admin\Views\Tags\Edit.cshtml"
         }))
    .Build());

            
            #line default
            #line hidden
WriteLiteral("\r\n</div>\r\n");

        }
    }
}
#pragma warning restore 1591