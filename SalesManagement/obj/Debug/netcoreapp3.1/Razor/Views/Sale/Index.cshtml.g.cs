#pragma checksum "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f1ad6a1fb6d57bcc31309fd18fc6cfc314abac49"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Sale_Index), @"mvc.1.0.view", @"/Views/Sale/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\_ViewImports.cshtml"
using SalesManagement;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\_ViewImports.cshtml"
using SalesManagement.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f1ad6a1fb6d57bcc31309fd18fc6cfc314abac49", @"/Views/Sale/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"790c74b91b249265140c3f9e21ac967e271703e6", @"/Views/_ViewImports.cshtml")]
    public class Views_Sale_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SalesManagement.Models.Sale>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>List Of Sales Transaction</h1>\r\n\r\n<p>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f1ad6a1fb6d57bcc31309fd18fc6cfc314abac493713", async() => {
                WriteLiteral("Create New");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 16 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.SaleID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n           \r\n            <th>\r\n                ");
#nullable restore
#line 20 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.ProductName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 23 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.CustomerName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            \r\n            <th>\r\n                ");
#nullable restore
#line 27 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Quantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 30 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Rate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 33 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.Total));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 36 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayNameFor(model => model.InvoiceID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 42 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 45 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.SaleID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 48 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.ProductName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 51 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.CustomerName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n           \r\n            <td>\r\n                ");
#nullable restore
#line 55 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Quantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 58 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Rate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 61 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.Total));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 64 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.InvoiceID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 67 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.ActionLink("Edit", "Edit", new { id = item.SaleID }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                ");
#nullable restore
#line 68 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
           Write(Html.ActionLink("Delete", "Delete", new { id = item.SaleID }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 71 "C:\Users\bibek\source\repos\SalesManagement\SalesManagement\Views\Sale\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SalesManagement.Models.Sale>> Html { get; private set; }
    }
}
#pragma warning restore 1591