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
    using BudgetOnline.UI.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DateEditBox.cshtml")]
    public partial class _Views_DateEditBox_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        
        #line 4 "..\..\Views\DateEditBox.cshtml"
            

        #line default
        #line hidden

#line 6 "..\..\Views\DateEditBox.cshtml"
public System.Web.WebPages.HelperResult Render(DateEditModel model) 
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 7 "..\..\Views\DateEditBox.cshtml"
 
    var id = string.IsNullOrWhiteSpace(model.Name) ? Html.GetUniqId(10) : model.Name;
    var value = model.Date.HasValue ? model.Date.Value.ToLocalTime().ToShortDateString() : string.Empty;
    


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <div");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 353), Tuple.Create("\"", 469)
, Tuple.Create(Tuple.Create("", 361), Tuple.Create("input-group", 361), true)

#line 11 "..\..\Views\DateEditBox.cshtml"
, Tuple.Create(Tuple.Create(" ", 372), Tuple.Create<System.Object, System.Int32>(model.Span > 0 ? "col-md-" + model.Span.ToString(CultureInfo.InvariantCulture) : string.Empty

#line default
#line hidden
, 373), false)
);

WriteLiteralTo(__razor_helper_writer, "> \r\n        <input");

WriteLiteralTo(__razor_helper_writer, " type=\"text\"");

WriteAttributeTo(__razor_helper_writer, "name", Tuple.Create(" name=\"", 500), Tuple.Create("\"", 510)

#line 12 "..\..\Views\DateEditBox.cshtml"
, Tuple.Create(Tuple.Create("", 507), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 507), false)
);

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 511), Tuple.Create("\"", 525)

#line 12 "..\..\Views\DateEditBox.cshtml"
, Tuple.Create(Tuple.Create("", 519), Tuple.Create<System.Object, System.Int32>(value

#line default
#line hidden
, 519), false)
);

WriteLiteralTo(__razor_helper_writer, " class=\"date-picker form-control\"");

WriteLiteralTo(__razor_helper_writer, " autocomplete=\"off\"");

WriteLiteralTo(__razor_helper_writer, "/>\r\n        <span");

WriteLiteralTo(__razor_helper_writer, " class=\"input-group-addon\"");

WriteLiteralTo(__razor_helper_writer, "><i");

WriteLiteralTo(__razor_helper_writer, " class=\"glyphicon glyphicon-calendar\"");

WriteLiteralTo(__razor_helper_writer, "></i></span>\r\n    </div>\r\n");


#line 15 "..\..\Views\DateEditBox.cshtml"


#line default
#line hidden
});

#line 15 "..\..\Views\DateEditBox.cshtml"
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
