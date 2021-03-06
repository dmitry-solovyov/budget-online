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
    
    #line 2 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
    using BudgetOnline.UI.PreCompiled.Models.ViewCommands;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class _Views_ListViewCommands_JsViewCommand_cshtml : System.Web.WebPages.HelperPage
    {
        
        #line 3 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
            

        #line default
        #line hidden

#line 5 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
public static System.Web.WebPages.HelperResult Render(ViewCommandUIModel command)
    {
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 6 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
     
        var jsCommand = command.Command as JsViewCommandModel;
        if (jsCommand == null)
        {
            throw new HttpException(404, "ViewCommand has invalid argument!");
        }
        var uid = Guid.NewGuid().ToString().Replace("-", "");
        


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <a");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 429), Tuple.Create("\"", 442)
, Tuple.Create(Tuple.Create("", 434), Tuple.Create("js", 434), true)

#line 14 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
, Tuple.Create(Tuple.Create("", 436), Tuple.Create<System.Object, System.Int32>(uid

#line default
#line hidden
, 436), false)
);

WriteLiteralTo(__razor_helper_writer, " href=\"#\"");

WriteAttributeTo(__razor_helper_writer, "title", Tuple.Create(" title=\"", 452), Tuple.Create("\"", 474)

#line 14 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
, Tuple.Create(Tuple.Create("", 460), Tuple.Create<System.Object, System.Int32>(command.Title

#line default
#line hidden
, 460), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 14 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
                       WriteTo(__razor_helper_writer, command.Text);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</a>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <script");

WriteLiteralTo(__razor_helper_writer, " type=\"text/javascript\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n        $().ready(function() {\r\n\t\t\t$(\'a#js");


#line 17 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
WriteTo(__razor_helper_writer, uid);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\').click(\r\n\t\t\t\tfunction(e) {\r\n\t\t\t\t\te.preventDefault();\r\n");


#line 20 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
					

#line default
#line hidden

#line 20 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
      if (!string.IsNullOrWhiteSpace(jsCommand.Data))
     {

#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n");

WriteLiteralTo(__razor_helper_writer, "                        ");


#line 22 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
WriteTo(__razor_helper_writer, jsCommand.ClientFunction);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "(\"");


#line 22 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
                       WriteTo(__razor_helper_writer, jsCommand.Data);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\");\r\n                    ");


#line 23 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
                           }
     else
     {

#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n");

WriteLiteralTo(__razor_helper_writer, "                        ");


#line 26 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
WriteTo(__razor_helper_writer, jsCommand.ClientFunction);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "();\r\n                    ");


#line 27 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
                           }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\t\t\t\t}\r\n\t\t\t);\r\n\t\t});\r\n\t\t\r\n    </script>\r\n");


#line 33 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"


#line default
#line hidden
});

#line 33 "..\..\Views\ListViewCommands\JsViewCommand.cshtml"
}
#line default
#line hidden

    }
}
#pragma warning restore 1591
