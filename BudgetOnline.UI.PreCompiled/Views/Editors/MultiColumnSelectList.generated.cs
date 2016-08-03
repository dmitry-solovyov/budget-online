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
    
    #line 1 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
    using BudgetOnline.UI.PreCompiled.Models.SelectItems;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Editors/MultiColumnSelectList.cshtml")]
    public partial class _Views_Editors_MultiColumnSelectList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        
        #line 2 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
            
    int GetRowsCount(int itemsCount, int columns)
    {
        return Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(itemsCount) / columns));
    }

        #line default
        #line hidden

#line 8 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
public System.Web.WebPages.HelperResult Render(SelectItemsModel model, string propertyName, string cssClass, bool multiSelect, int columns)
    {
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 9 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
     
    int rows = GetRowsCount(model.Items.Count(), columns);
    for (int r = 0; r < rows; r++)
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <div");

WriteLiteralTo(__razor_helper_writer, " class=\"row visible-sm visible-xs\"");

WriteLiteralTo(__razor_helper_writer, " style=\"height: 10px\"");

WriteLiteralTo(__razor_helper_writer, "></div>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"row\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");


#line 15 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
        

#line default
#line hidden

#line 15 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
          
        for (int c = 0; c < columns; c++)
        {
            var index = r * columns + c;
            if (index < model.Items.Count())
            {
                var item = model.Items.ElementAt(index);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <div");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 786), Tuple.Create("\"", 866)
, Tuple.Create(Tuple.Create("", 794), Tuple.Create("col-sm-6", 794), true)
, Tuple.Create(Tuple.Create(" ", 802), Tuple.Create("col-xs-8", 803), true)
, Tuple.Create(Tuple.Create(" ", 811), Tuple.Create("col-md-", 812), true)

#line 22 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
, Tuple.Create(Tuple.Create("", 819), Tuple.Create<System.Object, System.Int32>(Convert.ToInt32(Math.Ceiling(12m / columns))

#line default
#line hidden
, 819), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n                <label");

WriteLiteralTo(__razor_helper_writer, " class=\"checkbox-inline\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                    <input");

WriteLiteralTo(__razor_helper_writer, " type=\"checkbox\"");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 961), Tuple.Create("\"", 977)

#line 24 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
, Tuple.Create(Tuple.Create("", 966), Tuple.Create<System.Object, System.Int32>(item.Value

#line default
#line hidden
, 966), false)
);

WriteAttributeTo(__razor_helper_writer, "name", Tuple.Create(" name=\"", 978), Tuple.Create("\"", 1015)

#line 24 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
, Tuple.Create(Tuple.Create("", 985), Tuple.Create<System.Object, System.Int32>(propertyName

#line default
#line hidden
, 985), false)
, Tuple.Create(Tuple.Create("", 1000), Tuple.Create("[", 1000), true)

#line 24 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
   , Tuple.Create(Tuple.Create("", 1001), Tuple.Create<System.Object, System.Int32>(item.Value

#line default
#line hidden
, 1001), false)
, Tuple.Create(Tuple.Create("", 1014), Tuple.Create("]", 1014), true)
);

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 1016), Tuple.Create("\"", 1034)

#line 24 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
                         , Tuple.Create(Tuple.Create("", 1024), Tuple.Create<System.Object, System.Int32>(item.Text

#line default
#line hidden
, 1024), false)
);

WriteLiteralTo(__razor_helper_writer, " ");


#line 24 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
                                                                                        WriteTo(__razor_helper_writer, new HtmlString(item.Selected ? "checked='checked'" : string.Empty));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "/>\r\n");

WriteLiteralTo(__razor_helper_writer, "                    ");


#line 25 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
WriteTo(__razor_helper_writer, item.Text);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n                </label>\r\n            </div>\r\n");


#line 28 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
            }
        }
        

#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n    </div>\r\n");


#line 32 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
    }
    
    
    

#line default
#line hidden

#line 40 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
               


#line default
#line hidden
});

#line 41 "..\..\Views\Editors\MultiColumnSelectList.cshtml"
}
#line default
#line hidden

        public _Views_Editors_MultiColumnSelectList_cshtml()
        {
        }
        public override void Execute()
        {
        }
    }
}
#pragma warning restore 1591