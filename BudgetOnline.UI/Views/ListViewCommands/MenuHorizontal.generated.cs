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
    
    #line 2 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
    using BudgetOnline.UI.Models.ViewCommands;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class _Views_ListViewCommands_MenuHorizontal_cshtml : System.Web.WebPages.HelperPage
    {
        
        #line 3 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
            

        #line default
        #line hidden

#line 5 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
public static System.Web.WebPages.HelperResult Render(IEnumerable<ViewCommandUIModelHelper> commands)
    {
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 6 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
     
        if (commands == null || !commands.Any())
        {
        }
        else
        { 
            foreach (var command in commands)
            {
    

#line default
#line hidden

#line 14 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
WriteTo(__razor_helper_writer, _Views_ListViewCommands_ViewCommandUI_cshtml.Render(command.GetResult(), true));


#line default
#line hidden

#line 14 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
                                                                                   
            }
        }


#line default
#line hidden
});

#line 17 "..\..\Views\ListViewCommands\MenuHorizontal.cshtml"
}
#line default
#line hidden

    }
}
#pragma warning restore 1591
