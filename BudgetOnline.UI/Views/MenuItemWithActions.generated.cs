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
    
    #line 1 "..\..\Views\MenuItemWithActions.cshtml"
    using BudgetOnline.UI.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/MenuItemWithActions.cshtml")]
    public partial class MenuItemWithActions : System.Web.Mvc.WebViewPage<dynamic>
    {
        
        #line 3 "..\..\Views\MenuItemWithActions.cshtml"


        #line default
        #line hidden

public System.Web.WebPages.HelperResult Render(PaginationModel model)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 7 "..\..\Views\MenuItemWithActions.cshtml"
 
    var id = string.IsNullOrWhiteSpace(model.Name) ? Html.GetUniqId(10) : model.Name;


#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, @"	<li class=""dropdown"">
		<a data-toggle=""dropdown"" class=""dropdown-toggle"" href=""#"">Title <b class=""caret""></b></a>
		<ul class=""dropdown-menu"">
			<li><a href=""#"">Action</a></li>
			<li><a href=""#"">Another action</a></li>
			<li><a href=""#"">Something else here</a></li>
			<li class=""divider""></li>
			");



WriteLiteralTo(@__razor_helper_writer, "\r\n\t\t\t");



WriteLiteralTo(@__razor_helper_writer, "\r\n\t\t\t<li><a href=\"#\">One more separated link</a></li>\r\n\t\t</ul>\r\n\t</li>\r\n");



#line 22 "..\..\Views\MenuItemWithActions.cshtml"

#line default
#line hidden

});

}


        public MenuItemWithActions()
        {
        }
        public override void Execute()
        {



WriteLiteral("\r\n");


WriteLiteral("\r\n\r\n");




        }
    }
}
#pragma warning restore 1591
