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
    
    #line 1 "..\..\Views\Editors\SelectList.cshtml"
    using BudgetOnline.UI.Models.SelectItems;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Editors/SelectList.cshtml")]
    public partial class _Views_Editors_SelectList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        
        #line 2 "..\..\Views\Editors\SelectList.cshtml"
            


        #line default
        #line hidden

#line 5 "..\..\Views\Editors\SelectList.cshtml"
public System.Web.WebPages.HelperResult Render(SelectItemsModel model, string propertyName, string cssClass, bool multiSelect)
    {
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 6 "..\..\Views\Editors\SelectList.cshtml"
     
    var id = (propertyName ?? string.Empty).Replace(".", "_");
        if (model == null)
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <select");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 279), Tuple.Create("\"", 296)

#line 10 "..\..\Views\Editors\SelectList.cshtml"
, Tuple.Create(Tuple.Create("", 287), Tuple.Create<System.Object, System.Int32>(cssClass

#line default
#line hidden
, 287), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n    </select>\r\n");


#line 12 "..\..\Views\Editors\SelectList.cshtml"
        }
        else if (multiSelect)
        {
            
    
    foreach (var item in model.Items)
    {
        //<option data-icon="@item.Icon" value="@item.Value" @(new HtmlString(item.Selected ? "selected='selected'" : string.Empty))>@item.Text</option>


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n        <label");

WriteLiteralTo(__razor_helper_writer, " class=\"checkbox form-inline\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n            <input");

WriteLiteralTo(__razor_helper_writer, " type=\"checkbox\"");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 688), Tuple.Create("\"", 704)

#line 22 "..\..\Views\Editors\SelectList.cshtml"
, Tuple.Create(Tuple.Create("", 693), Tuple.Create<System.Object, System.Int32>(item.Value

#line default
#line hidden
, 693), false)
);

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 705), Tuple.Create("\"", 724)

#line 22 "..\..\Views\Editors\SelectList.cshtml"
, Tuple.Create(Tuple.Create("", 713), Tuple.Create<System.Object, System.Int32>(item.Value

#line default
#line hidden
, 713), false)
);

WriteLiteralTo(__razor_helper_writer, " ");


#line 22 "..\..\Views\Editors\SelectList.cshtml"
                                           WriteTo(__razor_helper_writer, new HtmlString(item.Selected ? "checked='checked'" : string.Empty));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "/>\r\n");

WriteLiteralTo(__razor_helper_writer, "            ");


#line 23 "..\..\Views\Editors\SelectList.cshtml"
WriteTo(__razor_helper_writer, item.Text);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n        </label>\r\n    </div>\r\n");


#line 26 "..\..\Views\Editors\SelectList.cshtml"
    }
    
            


#line default
#line hidden

#line 29 "..\..\Views\Editors\SelectList.cshtml"
                                                            
        }
        else
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <select");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 989), Tuple.Create("\"", 1019)
, Tuple.Create(Tuple.Create("", 997), Tuple.Create("form-control", 997), true)

#line 33 "..\..\Views\Editors\SelectList.cshtml"
, Tuple.Create(Tuple.Create(" ", 1009), Tuple.Create<System.Object, System.Int32>(cssClass

#line default
#line hidden
, 1010), false)
);

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 1020), Tuple.Create("\"", 1028)

#line 33 "..\..\Views\Editors\SelectList.cshtml"
, Tuple.Create(Tuple.Create("", 1025), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 1025), false)
);

WriteAttributeTo(__razor_helper_writer, "name", Tuple.Create(" name=\"", 1029), Tuple.Create("\"", 1049)

#line 33 "..\..\Views\Editors\SelectList.cshtml"
, Tuple.Create(Tuple.Create("", 1036), Tuple.Create<System.Object, System.Int32>(propertyName

#line default
#line hidden
, 1036), false)
);

WriteLiteralTo(__razor_helper_writer, " ");


#line 33 "..\..\Views\Editors\SelectList.cshtml"
                                            WriteTo(__razor_helper_writer, new HtmlString(multiSelect ? "multiple='multiple'" : string.Empty));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, ">\r\n");


#line 34 "..\..\Views\Editors\SelectList.cshtml"
        

#line default
#line hidden

#line 34 "..\..\Views\Editors\SelectList.cshtml"
         foreach (var item in model.Items)
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <option");

WriteLiteralTo(__razor_helper_writer, " data-icon=\"");


#line 36 "..\..\Views\Editors\SelectList.cshtml"
 WriteTo(__razor_helper_writer, item.Icon);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\"");

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 1220), Tuple.Create("\"", 1239)

#line 36 "..\..\Views\Editors\SelectList.cshtml"
, Tuple.Create(Tuple.Create("", 1228), Tuple.Create<System.Object, System.Int32>(item.Value

#line default
#line hidden
, 1228), false)
);

WriteLiteralTo(__razor_helper_writer, " ");


#line 36 "..\..\Views\Editors\SelectList.cshtml"
                                  WriteTo(__razor_helper_writer, new HtmlString(item.Selected ? "selected='selected'" : string.Empty));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, ">");


#line 36 "..\..\Views\Editors\SelectList.cshtml"
                                                                                                         WriteTo(__razor_helper_writer, item.Text);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</option>\r\n");


#line 37 "..\..\Views\Editors\SelectList.cshtml"
        }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    </select>\r\n");


#line 39 "..\..\Views\Editors\SelectList.cshtml"
            


#line default
#line hidden

#line 40 "..\..\Views\Editors\SelectList.cshtml"
                                                            
        }


#line default
#line hidden
});

#line 42 "..\..\Views\Editors\SelectList.cshtml"
}
#line default
#line hidden

        public _Views_Editors_SelectList_cshtml()
        {
        }
        public override void Execute()
        {
        }
    }
}
#pragma warning restore 1591
