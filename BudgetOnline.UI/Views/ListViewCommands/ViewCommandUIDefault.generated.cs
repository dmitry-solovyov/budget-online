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
    
    #line 2 "..\..\Views\ListViewCommands\ViewCommandUIDefault.cshtml"
    using BudgetOnline.UI.Models.ViewCommands;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class _Views_ListViewCommands_ViewCommandUIDefault_cshtml : System.Web.WebPages.HelperPage
    {
        
        #line 3 "..\..\Views\ListViewCommands\ViewCommandUIDefault.cshtml"
            

        #line default
        #line hidden

#line 5 "..\..\Views\ListViewCommands\ViewCommandUIDefault.cshtml"
public static System.Web.WebPages.HelperResult Render(ViewCommandUIModel command)
    {
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 6 "..\..\Views\ListViewCommands\ViewCommandUIDefault.cshtml"
     
        if (command.IsVisible)
        {
            var iconForDefaultCommand = command.IconCssClass;
            if (string.IsNullOrWhiteSpace(iconForDefaultCommand))
            {
                iconForDefaultCommand = "glyphicon-list";
            }

            
            if (command.Command.CommandType == CommandType.Post)
            {
            }
            else if (command.Command.CommandType == CommandType.Redirect)
            {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "<a");

WriteLiteralTo(__razor_helper_writer, " class=\"btn btn-default btn-xs\"");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 637), Tuple.Create("\"", 697)

#line 21 "..\..\Views\ListViewCommands\ViewCommandUIDefault.cshtml"
, Tuple.Create(Tuple.Create("", 644), Tuple.Create<System.Object, System.Int32>((command.Command as RedirectViewCommandModel).Path

#line default
#line hidden
, 644), false)
);

WriteLiteralTo(__razor_helper_writer, "><i");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 701), Tuple.Create("\"", 741)
, Tuple.Create(Tuple.Create("", 709), Tuple.Create("glyphicon", 709), true)

#line 21 "..\..\Views\ListViewCommands\ViewCommandUIDefault.cshtml"
                                   , Tuple.Create(Tuple.Create(" ", 718), Tuple.Create<System.Object, System.Int32>(iconForDefaultCommand

#line default
#line hidden
, 719), false)
);

WriteLiteralTo(__razor_helper_writer, "></i><span");

WriteLiteralTo(__razor_helper_writer, " style=\"padding-left: 1em\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 21 "..\..\Views\ListViewCommands\ViewCommandUIDefault.cshtml"
                                                                                                                                                 WriteTo(__razor_helper_writer, command.Text);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</span></a>\r\n");


#line 22 "..\..\Views\ListViewCommands\ViewCommandUIDefault.cshtml"
            }
        }


#line default
#line hidden
});

#line 24 "..\..\Views\ListViewCommands\ViewCommandUIDefault.cshtml"
}
#line default
#line hidden

    }
}
#pragma warning restore 1591
