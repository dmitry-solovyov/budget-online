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
    
    #line 1 "..\..\Views\AutoCompleteTextBox.cshtml"
    using BudgetOnline.UI.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/AutoCompleteTextBox.cshtml")]
    public partial class _Views_AutoCompleteTextBox_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        
        #line 3 "..\..\Views\AutoCompleteTextBox.cshtml"
            

        #line default
        #line hidden

#line 6 "..\..\Views\AutoCompleteTextBox.cshtml"
public System.Web.WebPages.HelperResult Render(AutoCompleteTextBoxModel model){
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 6 "..\..\Views\AutoCompleteTextBox.cshtml"
                                               
	var id = string.IsNullOrWhiteSpace(model.Name) ? Html.GetUniqId(10) : model.Name;
    


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\t<input");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 276), Tuple.Create("\"", 284)

#line 9 "..\..\Views\AutoCompleteTextBox.cshtml"
, Tuple.Create(Tuple.Create("", 281), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 281), false)
);

WriteLiteralTo(__razor_helper_writer, " class=\"form-control\"");

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 306), Tuple.Create("\"", 328)

#line 9 "..\..\Views\AutoCompleteTextBox.cshtml"
, Tuple.Create(Tuple.Create("", 314), Tuple.Create<System.Object, System.Int32>(model.Value

#line default
#line hidden
, 314), false)
);

WriteAttributeTo(__razor_helper_writer, "name", Tuple.Create(" name=\"", 329), Tuple.Create("\"", 339)

#line 9 "..\..\Views\AutoCompleteTextBox.cshtml"
, Tuple.Create(Tuple.Create("", 336), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 336), false)
);

WriteLiteralTo(__razor_helper_writer, " />\r\n");

WriteLiteralTo(__razor_helper_writer, "\t<script");

WriteLiteralTo(__razor_helper_writer, " type=\"text/javascript\"");

WriteLiteralTo(__razor_helper_writer, " language=\"javascript\"");

WriteLiteralTo(__razor_helper_writer, "><!--\r\n\t\t$(function () {\r\n\t\t\tfunction split(val) {\r\n\t\t\t\treturn val.split(/,\\s*/);" +
"\r\n\t\t\t}\r\n\t\t\tfunction extractLast(term) {\r\n\t\t\t\treturn split(term).pop();\r\n\t\t\t}\r\n\t\t" +
"\t$(\"#");


#line 18 "..\..\Views\AutoCompleteTextBox.cshtml"
WriteTo(__razor_helper_writer, id);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, @""")
				// don't navigate away from the field on tab when selecting an item
				.bind(""keydown"", function (event) {
					if (event.keyCode === $.ui.keyCode.TAB &&
						$(this).data(""ui-autocomplete"").menu.active) {
						event.preventDefault();
					}
				})
				.autocomplete({
					source: function (request, response) {
						$.getJSON('");


#line 28 "..\..\Views\AutoCompleteTextBox.cshtml"
WriteTo(__razor_helper_writer, model.RequestUrl);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, @"', {
							term: extractLast(request.term)
						}, response);
					},
					search: function () {
						// custom minLength
						var term = extractLast(this.value);
						if (term.length < 2) {
							return false;
						}
						return true;
					},
					focus: function () {
						// prevent value inserted on focus
						return false;
					},
					select: function (event, ui) {
						var terms = split(this.value);
						// remove the current input
						terms.pop();
						// add the selected item
						terms.push(ui.item.value);
						// add placeholder to get the comma-and-space at the end
						terms.push("""");
						this.value = terms.join("", "");
						return false;
					}
				});
		});
		//-->
	</script>
");


#line 59 "..\..\Views\AutoCompleteTextBox.cshtml"


#line default
#line hidden
});

#line 59 "..\..\Views\AutoCompleteTextBox.cshtml"
}
#line default
#line hidden

        public _Views_AutoCompleteTextBox_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
