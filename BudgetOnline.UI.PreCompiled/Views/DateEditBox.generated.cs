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
    
    #line 1 "..\..\Views\DateEditBox.cshtml"
    using System.Globalization;
    
    #line default
    #line hidden
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
    
    #line 2 "..\..\Views\DateEditBox.cshtml"
    using BudgetOnline.UI.PreCompiled.Extensions;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\DateEditBox.cshtml"
    using BudgetOnline.UI.PreCompiled.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DateEditBox.cshtml")]
    public partial class _Views_DateEditBox_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        
        #line 5 "..\..\Views\DateEditBox.cshtml"
            

        #line default
        #line hidden

#line 7 "..\..\Views\DateEditBox.cshtml"
public System.Web.WebPages.HelperResult Render(DateEditModel model) 
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 8 "..\..\Views\DateEditBox.cshtml"
 
    var id = string.IsNullOrWhiteSpace(model.Name) ? Html.GetUniqId(10) : model.Name;
    var value = model.Date.HasValue ? model.Date.Value.ToLocalTime().ToShortDateString() : string.Empty;
    


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <div");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 412), Tuple.Create("\"", 528)
, Tuple.Create(Tuple.Create("", 420), Tuple.Create("input-group", 420), true)

#line 12 "..\..\Views\DateEditBox.cshtml"
, Tuple.Create(Tuple.Create(" ", 431), Tuple.Create<System.Object, System.Int32>(model.Span > 0 ? "col-md-" + model.Span.ToString(CultureInfo.InvariantCulture) : string.Empty

#line default
#line hidden
, 432), false)
);

WriteLiteralTo(__razor_helper_writer, "> \r\n        <input");

WriteLiteralTo(__razor_helper_writer, " type=\"text\"");

WriteAttributeTo(__razor_helper_writer, "name", Tuple.Create(" name=\"", 559), Tuple.Create("\"", 569)

#line 13 "..\..\Views\DateEditBox.cshtml"
, Tuple.Create(Tuple.Create("", 566), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 566), false)
);

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 570), Tuple.Create("\"", 584)

#line 13 "..\..\Views\DateEditBox.cshtml"
, Tuple.Create(Tuple.Create("", 578), Tuple.Create<System.Object, System.Int32>(value

#line default
#line hidden
, 578), false)
);

WriteLiteralTo(__razor_helper_writer, " class=\"date-picker form-control\"");

WriteLiteralTo(__razor_helper_writer, " autocomplete=\"off\"");

WriteLiteralTo(__razor_helper_writer, "/>\r\n        <span");

WriteLiteralTo(__razor_helper_writer, " class=\"input-group-addon\"");

WriteLiteralTo(__razor_helper_writer, "><i");

WriteLiteralTo(__razor_helper_writer, " class=\"glyphicon glyphicon-calendar\"");

WriteLiteralTo(__razor_helper_writer, "></i></span>\r\n    </div>\r\n");


#line 16 "..\..\Views\DateEditBox.cshtml"


#line default
#line hidden
});

#line 16 "..\..\Views\DateEditBox.cshtml"
}
#line default
#line hidden

        public _Views_DateEditBox_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
