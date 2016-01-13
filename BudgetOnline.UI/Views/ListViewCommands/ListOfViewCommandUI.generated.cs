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
    
    #line 1 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
    using BudgetOnline.UI.Models.ViewCommands;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ListViewCommands/ListOfViewCommandUI.cshtml")]
    public partial class _Views_ListViewCommands_ListOfViewCommandUI_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {

#line 3 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
public System.Web.WebPages.HelperResult Render(IEnumerable<ViewCommandUIModel> commands)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 4 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
 
    if (commands == null || !commands.Any())
    {
    }
    else
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <div");

WriteLiteralTo(__razor_helper_writer, " class=\"btn-group\"");

WriteLiteralTo(__razor_helper_writer, "> \r\n");


#line 11 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
            

#line default
#line hidden

#line 11 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
              
                ViewCommandUIModel defaultCommand = commands.FirstOrDefault(o => o.IsDefault && o.Command.CommandType == CommandType.Redirect);
                if (defaultCommand != null)
                {
                    

#line default
#line hidden

#line 15 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
WriteTo(__razor_helper_writer, _Views_ListViewCommands_ViewCommandUIDefault_cshtml.Render(defaultCommand));


#line default
#line hidden

#line 15 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
                                                                                               

				

#line default
#line hidden

#line 17 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
                                                                                                                                                                                                       
                }
                else
                {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                    <button");

WriteLiteralTo(__razor_helper_writer, " class=\"btn btn-default\"");

WriteLiteralTo(__razor_helper_writer, "><i");

WriteLiteralTo(__razor_helper_writer, " class=\"glyphicon glyphicon-list\"");

WriteLiteralTo(__razor_helper_writer, "></i><span");

WriteLiteralTo(__razor_helper_writer, " style=\"padding-left: 1em\"");

WriteLiteralTo(__razor_helper_writer, ">Команды</span></button>\r\n");


#line 22 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
                }
            

#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n            <a");

WriteLiteralTo(__razor_helper_writer, " class=\"btn btn-default dropdown-toggle btn-xs\"");

WriteLiteralTo(__razor_helper_writer, " data-toggle=\"dropdown\"");

WriteLiteralTo(__razor_helper_writer, "><span");

WriteLiteralTo(__razor_helper_writer, " class=\"caret\"");

WriteLiteralTo(__razor_helper_writer, "></span></a>\r\n            <ul");

WriteLiteralTo(__razor_helper_writer, " class=\"dropdown-menu\"");

WriteLiteralTo(__razor_helper_writer, " role=\"menu\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");


#line 26 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
                

#line default
#line hidden

#line 26 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
                 foreach (ViewCommandUIModel command in commands)
                {
                    

#line default
#line hidden

#line 28 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
WriteTo(__razor_helper_writer, _Views_ListViewCommands_ViewCommandUI_cshtml.Render(command, false));


#line default
#line hidden

#line 28 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
                                                                                        
                }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            </ul>\r\n        </div>\r\n");


#line 32 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
    }


#line default
#line hidden
});

#line 33 "..\..\Views\ListViewCommands\ListOfViewCommandUI.cshtml"
}
#line default
#line hidden

        public _Views_ListViewCommands_ListOfViewCommandUI_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
