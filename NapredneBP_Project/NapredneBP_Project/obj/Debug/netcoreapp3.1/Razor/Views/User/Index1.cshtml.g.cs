#pragma checksum "D:\7. semestar\Napredne baze podataka\neo i redis projekat\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\User\Index1.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8ce45574097fc97077a55c0eb3597693ef4573d9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Index1), @"mvc.1.0.view", @"/Views/User/Index1.cshtml")]
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
#line 1 "D:\7. semestar\Napredne baze podataka\neo i redis projekat\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\_ViewImports.cshtml"
using NapredneBP_Project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\7. semestar\Napredne baze podataka\neo i redis projekat\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\_ViewImports.cshtml"
using NapredneBP_Project.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8ce45574097fc97077a55c0eb3597693ef4573d9", @"/Views/User/Index1.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67e24c10cb05e36984a1a36a31d216175c0bfda3", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Index1 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("nav-link "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Person", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "GetAllPersons", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 1 "D:\7. semestar\Napredne baze podataka\neo i redis projekat\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\User\Index1.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">Welcome to IMDB <i>Clone</i></h1>\r\n    <p class=\"text-muted h4\">Your favorite movies all in one place</p>\r\n</div>\r\n\r\n<br />\r\n\r\n<div class=\"text-center\">\r\n    <h3>");
#nullable restore
#line 14 "D:\7. semestar\Napredne baze podataka\neo i redis projekat\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\User\Index1.cshtml"
   Write(Html.ActionLink("Logout", "Logout", "User"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n</div>\r\n\r\n<br />\r\n\r\n<div class=\"text-center\">\r\n    <h3>");
#nullable restore
#line 20 "D:\7. semestar\Napredne baze podataka\neo i redis projekat\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\User\Index1.cshtml"
   Write(Html.ActionLink("Show all movies", "AllMovies", "Movie"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n</div>\r\n\r\n<br />\r\n\r\n<div class=\"text-center\">\r\n    <h3>");
#nullable restore
#line 26 "D:\7. semestar\Napredne baze podataka\neo i redis projekat\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\User\Index1.cshtml"
   Write(Html.ActionLink("Create new movie", "Index", "Movie"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n</div>\r\n\r\n<br />\r\n\r\n<div class=\"text-center\">\r\n    <h3>");
#nullable restore
#line 32 "D:\7. semestar\Napredne baze podataka\neo i redis projekat\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\User\Index1.cshtml"
   Write(Html.ActionLink("Search movies", "GMBT", "Movie"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n</div>\r\n\r\n<br />\r\n\r\n<!--<div class=\"text-center\">\r\n    <h3>");
            WriteLiteral("</h3>\r\n</div>-->\r\n\r\n");
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h3>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8ce45574097fc97077a55c0eb3597693ef4573d96673", async() => {
                WriteLiteral("Show all persons");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </h3>\r\n</div>\r\n\r\n<br />\r\n\r\n<div class=\"text-center\">\r\n    <h3>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8ce45574097fc97077a55c0eb3597693ef4573d98219", async() => {
                WriteLiteral("Create new person");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </h3>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591