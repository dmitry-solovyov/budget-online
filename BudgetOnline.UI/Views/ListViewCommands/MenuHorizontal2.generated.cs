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

namespace BudgetOnline.UI.Views.ListViewCommands
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
    
    #line 6 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
    using BudgetOnline.UI.Models.ViewCommands;
    
    #line default
    #line hidden
    
    #line 7 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
    using BudgetOnline.UI.Views.ListViewCommands;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class MenuHorizontal : System.Web.WebPages.HelperPage
    {

#line 9 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
public static System.Web.WebPages.HelperResult Render(IEnumerable<ViewCommandUIModelHelper> commands)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 10 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
 
    if (commands != null)
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <div");

WriteLiteralTo(__razor_helper_writer, " class=\"collapse navbar-collapse\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n            <ul");

WriteLiteralTo(__razor_helper_writer, " class=\"nav navbar-nav\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n\r\n");


#line 16 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
                

#line default
#line hidden

#line 16 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
                 foreach (var command in commands)
                {
                    

#line default
#line hidden

#line 18 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
WriteTo(__razor_helper_writer, ViewCommandUI.Render(command.GetResult(), true));


#line default
#line hidden

#line 18 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
                                                                    
                }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n            </ul>\r\n        </div>\r\n");


#line 23 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
    }


#line default
#line hidden
});

#line 24 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
}
#line default
#line hidden

    }
}
#pragma warning restore 1591