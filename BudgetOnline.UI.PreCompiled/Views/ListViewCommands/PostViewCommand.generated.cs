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
    
    #line 2 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
    using BudgetOnline.UI.PreCompiled.Models.ViewCommands;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class _Views_ListViewCommands_PostViewCommand_cshtml : System.Web.WebPages.HelperPage
    {
        
        #line 3 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
            

        #line default
        #line hidden

#line 5 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
public static System.Web.WebPages.HelperResult Render(ViewCommandUIModel command)
    {
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 6 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
     
        var postCommand = command.Command as PostViewCommandModel;
        if (postCommand == null)
        {
            throw new HttpException(404, "ViewCommand has invalid argument!");
        }

        var uid = Guid.NewGuid().ToString().Replace("-", "");
        


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <a");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 437), Tuple.Create("\"", 452)
, Tuple.Create(Tuple.Create("", 442), Tuple.Create("post", 442), true)

#line 15 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
, Tuple.Create(Tuple.Create("", 446), Tuple.Create<System.Object, System.Int32>(uid

#line default
#line hidden
, 446), false)
);

WriteLiteralTo(__razor_helper_writer, " role=\"menuitem\"");

WriteLiteralTo(__razor_helper_writer, " tabindex=\"-1\"");

WriteLiteralTo(__razor_helper_writer, " href=\"#\"");

WriteAttributeTo(__razor_helper_writer, "title", Tuple.Create(" title=\"", 492), Tuple.Create("\"", 514)

#line 15 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
, Tuple.Create(Tuple.Create("", 500), Tuple.Create<System.Object, System.Int32>(command.Title

#line default
#line hidden
, 500), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 15 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
                                                       WriteTo(__razor_helper_writer, command.Text);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</a>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <script");

WriteLiteralTo(__razor_helper_writer, " type=\"text/javascript\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n        $().ready(function() {\r\n\t\t\t$(\'a#post");


#line 18 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
WriteTo(__razor_helper_writer, uid);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\').click(\r\n\t\t\t\tfunction(e) {\r\n\t\t\t        e.preventDefault();\r\n\t\t\t        $.postda" +
"tas({\r\n\t\t\t\t        url:\'");


#line 22 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
WriteTo(__razor_helper_writer, postCommand.Path);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\',\r\n\t\t\t\t        datas:[\r\n");


#line 24 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
					        

#line default
#line hidden

#line 24 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
              if (postCommand.Parameters != null && postCommand.Parameters.Any())
             {
                 foreach (var parameter in postCommand.Parameters)
                 {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                                ");

WriteLiteralTo(__razor_helper_writer, "\r\n                                {name:\"");


#line 29 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
          WriteTo(__razor_helper_writer, parameter.Key);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\",value:\"");


#line 29 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
                                   WriteTo(__razor_helper_writer, parameter.Value);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\"}  ");


#line 29 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
                                                         WriteTo(__razor_helper_writer, parameter.Key == postCommand.Parameters.Last().Key ? "" : ",");


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n                                ");

WriteLiteralTo(__razor_helper_writer, "\r\n");


#line 31 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
                 }
             }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\t\t\t\t               ]\r\n\t\t\t        });\r\n\t\t\t\t}\r\n\t\t\t);\r\n\t\t});\r\n\t\t\r\n    </script>\r\n");


#line 40 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"


#line default
#line hidden
});

#line 40 "..\..\Views\ListViewCommands\PostViewCommand.cshtml"
}
#line default
#line hidden

    }
}
#pragma warning restore 1591
