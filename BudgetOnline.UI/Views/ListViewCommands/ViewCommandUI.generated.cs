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
    
    #line 2 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
    using BudgetOnline.UI.Models.ViewCommands;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class _Views_ListViewCommands_ViewCommandUI_cshtml : System.Web.WebPages.HelperPage
    {
        
        #line 3 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
            

        #line default
        #line hidden

#line 5 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
public static System.Web.WebPages.HelperResult Render(ViewCommandUIModel command, bool isRoot)
    {
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 6 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
     
        if (command.IsVisible)
        {
            if (command.IsDividerBefore)
            {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <li");

WriteLiteralTo(__razor_helper_writer, " class=\"divider\"");

WriteLiteralTo(__razor_helper_writer, "></li>\r\n");


#line 12 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
            }

            if (command.ChildCommands != null && command.ChildCommands.Any())
            {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <li");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 402), Tuple.Create("\"", 446)
, Tuple.Create(Tuple.Create("", 410), Tuple.Create("dropdown", 410), true)

#line 16 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
, Tuple.Create(Tuple.Create("", 418), Tuple.Create<System.Object, System.Int32>(!isRoot ? "-submenu" : ""

#line default
#line hidden
, 418), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n        <a");

WriteLiteralTo(__razor_helper_writer, " data-toggle=\"dropdown\"");

WriteLiteralTo(__razor_helper_writer, " class=\"dropdown-toggle\"");

WriteLiteralTo(__razor_helper_writer, " href=\"#\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 17 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
                                     WriteTo(__razor_helper_writer, command.Text);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n");


#line 18 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
                    

#line default
#line hidden

#line 18 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
                     if (isRoot)
                    {
                    }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                    <b");

WriteLiteralTo(__razor_helper_writer, " class=\"caret\"");

WriteLiteralTo(__razor_helper_writer, "></b></a>\r\n        <ul");

WriteLiteralTo(__razor_helper_writer, " class=\"dropdown-menu\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");


#line 23 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
            

#line default
#line hidden

#line 23 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
             foreach (var childItem in command.ChildCommands)
            {
                

#line default
#line hidden

#line 25 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
WriteTo(__razor_helper_writer, ViewCommandUI.Render(childItem, false));


#line default
#line hidden

#line 25 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
                                                         
            }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        </ul>\r\n    </li>\r\n");


#line 29 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
            }
            else if (command.Command.CommandType == CommandType.Post)
            {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <li");

WriteLiteralTo(__razor_helper_writer, " role=\"presentation\"");

WriteLiteralTo(__razor_helper_writer, " class=\"t3\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 32 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
         WriteTo(__razor_helper_writer, PostViewCommand.Render(command));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 33 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
            }
            else if (command.Command.CommandType == CommandType.Redirect)
            {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <li");

WriteLiteralTo(__razor_helper_writer, " role=\"presentation\"");

WriteLiteralTo(__razor_helper_writer, " class=\"t4\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 36 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
         WriteTo(__razor_helper_writer, RedirectViewCommand.Render(command));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 37 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
            }
            else if (command.Command.CommandType == CommandType.Js)
            {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <li");

WriteLiteralTo(__razor_helper_writer, " role=\"presentation\"");

WriteLiteralTo(__razor_helper_writer, " class=\"t4\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 40 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
         WriteTo(__razor_helper_writer, JsViewCommand.Render(command));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 41 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
            }

            if (command.IsDividerAfter)
            {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <li");

WriteLiteralTo(__razor_helper_writer, " class=\"divider\"");

WriteLiteralTo(__razor_helper_writer, "></li>\r\n");


#line 46 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
            }
        }


#line default
#line hidden
});

#line 48 "..\..\Views\ListViewCommands\ViewCommandUI.cshtml"
}
#line default
#line hidden

    }
}
#pragma warning restore 1591
