﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1008
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BudgetOnline.UI.Views.Editors
{
    using System;
    using System.Collections.Generic;
    
    #line 2 "..\..\Views\Editors\CurrencyBundle.cshtml"
    using System.Globalization;
    
    #line default
    #line hidden
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    
    #line 1 "..\..\Views\Editors\CurrencyBundle.cshtml"
    using System.Web.Mvc;
    
    #line default
    #line hidden
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Editors/CurrencyBundle.cshtml")]
    public partial class CurrencyBundle : System.Web.Mvc.WebViewPage<dynamic>
    {
        
        #line 3 "..\..\Views\Editors\CurrencyBundle.cshtml"


        #line default
        #line hidden

public System.Web.WebPages.HelperResult Render(BudgetOnline.UI.Models.Editors.CurrencyBundle model, string propertyName, ModelState state, IDictionary<string, object> additionalValues)
    {
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 6 "..\..\Views\Editors\CurrencyBundle.cshtml"
     
        var id = Html.GetUniqId(10);

        var placeholder = "";
        if (additionalValues.ContainsKey("placeholder"))
        {
            placeholder = additionalValues["placeholder"].ToString();
        }

        var value = model.Sum.ToString(CultureInfo.CurrentUICulture);

        var cssFormulaClass = " span10 data-type-calculator input-block-level";
        if (state != null && state.Errors.Count > 0)
        {
            cssFormulaClass += " input-validation-error";
        }

        var cssCurrencyListClass = " data-type-calculator input-block-level";
        if (state != null && state.Errors.Count > 0)
        {
            cssCurrencyListClass += " input-validation-error";
        }

        var cssAccountListClass = " data-type-calculator input-block-level";
        if (state != null && state.Errors.Count > 0)
        {
            cssAccountListClass += " input-validation-error";
        }


#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div id=\"");



#line 35 "..\..\Views\Editors\CurrencyBundle.cshtml"
WriteTo(@__razor_helper_writer, id);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\">\r\n        <div class=\"row-fluid\">\r\n            <div class=\"span4\">\r\n           " +
"     ");



#line 38 "..\..\Views\Editors\CurrencyBundle.cshtml"
WriteTo(@__razor_helper_writer, new BudgetOnline.UI.Views.Editors.SelectList().Render(model.Account.Items, propertyName + ".Account", cssAccountListClass, false));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\r\n            </div>\r\n            <div class=\"span1\">\r\n            </div>\r\n      " +
"      <div class=\"span4\">\r\n                <div class=\"row-fluid\">\r\n            " +
"        <div class=\"input-append span12\">\r\n                        <input type=\"" +
"text\" name=\"");



#line 45 "..\..\Views\Editors\CurrencyBundle.cshtml"
                   WriteTo(@__razor_helper_writer, propertyName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, ".Formula\" id=\"");



#line 45 "..\..\Views\Editors\CurrencyBundle.cshtml"
                                                WriteTo(@__razor_helper_writer, propertyName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "_Formula\" value=\"");



#line 45 "..\..\Views\Editors\CurrencyBundle.cshtml"
                                                                                WriteTo(@__razor_helper_writer, model.Formula);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\" class=\"");



#line 45 "..\..\Views\Editors\CurrencyBundle.cshtml"
                                                                                                        WriteTo(@__razor_helper_writer, cssFormulaClass);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\" autocomplete = \"off\" placeholder=\"");



#line 45 "..\..\Views\Editors\CurrencyBundle.cshtml"
                                                                                                                                                            WriteTo(@__razor_helper_writer, placeholder);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, @"""/>
                        <span class=""add-on""><i class=""icon-comment""></i></span>
                    </div>
                </div>
                <div class=""row-fluid"">
                    <div class=""span12"">
                        <input type=""text"" name=""");



#line 51 "..\..\Views\Editors\CurrencyBundle.cshtml"
                   WriteTo(@__razor_helper_writer, propertyName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, ".Sum\" id=\"");



#line 51 "..\..\Views\Editors\CurrencyBundle.cshtml"
                                            WriteTo(@__razor_helper_writer, propertyName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "_Sum\" value=\"");



#line 51 "..\..\Views\Editors\CurrencyBundle.cshtml"
                                                                       WriteTo(@__razor_helper_writer, value);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\" class=\"calculator-result span10\" readonly = \"readonly\" autocomplete = \"off\" tit" +
"le = \"Результат из поля Сумма\"/>\r\n                    </div>\r\n                </" +
"div>\r\n            </div>\r\n            <div class=\"span3\">\r\n                ");



#line 56 "..\..\Views\Editors\CurrencyBundle.cshtml"
WriteTo(@__razor_helper_writer, new BudgetOnline.UI.Views.Editors.SelectList().Render(model.Currency.Items, propertyName + ".Currency", cssCurrencyListClass, false));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\r\n            </div>\r\n        </div>\r\n    </div>\r\n");



WriteLiteralTo(@__razor_helper_writer, "    <script type=\"text/javascript\">\r\n        $(document).ready(function () {\r\n   " +
"         turnOn_calculator(\'");



#line 62 "..\..\Views\Editors\CurrencyBundle.cshtml"
 WriteTo(@__razor_helper_writer, id);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\');\r\n        });\r\n    </script>\r\n");



#line 65 "..\..\Views\Editors\CurrencyBundle.cshtml"

#line default
#line hidden

});

}


        public CurrencyBundle()
        {
        }
        public override void Execute()
        {



WriteLiteral("\r\n");




        }
    }
}
#pragma warning restore 1591
