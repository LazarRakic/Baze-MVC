#pragma checksum "E:\Lazar\Elfak\4. godina\Napredne baze podataka\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\User\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "36d9577ffdb4c776a24cd6be6a90d9c8a84fee86"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Index), @"mvc.1.0.view", @"/Views/User/Index.cshtml")]
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
#line 1 "E:\Lazar\Elfak\4. godina\Napredne baze podataka\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\_ViewImports.cshtml"
using NapredneBP_Project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Lazar\Elfak\4. godina\Napredne baze podataka\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\_ViewImports.cshtml"
using NapredneBP_Project.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"36d9577ffdb4c776a24cd6be6a90d9c8a84fee86", @"/Views/User/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67e24c10cb05e36984a1a36a31d216175c0bfda3", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NapredneBP_Project.Models.User>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    <div class=\"text-center\">\r\n        <h3>");
#nullable restore
#line 4 "E:\Lazar\Elfak\4. godina\Napredne baze podataka\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\User\Index.cshtml"
       Write(Html.ActionLink("Register", "Create1", "User"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n    </div>\r\n");
            WriteLiteral("    <br />\r\n");
            WriteLiteral("    <div class=\"text-center\">\r\n        <h3>");
#nullable restore
#line 10 "E:\Lazar\Elfak\4. godina\Napredne baze podataka\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\User\Index.cshtml"
       Write(Html.ActionLink("Login", "Login1", "User"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n    </div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NapredneBP_Project.Models.User> Html { get; private set; }
    }
}
#pragma warning restore 1591
