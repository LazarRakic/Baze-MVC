#pragma checksum "C:\Users\Teodora\Desktop\7. semestar\Napredne baze podataka\projekat 1\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\Movie\AllMovies.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5fe33ee35c4132a2dfc332e0862d17bf993cbece"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Movie_AllMovies), @"mvc.1.0.view", @"/Views/Movie/AllMovies.cshtml")]
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
#line 1 "C:\Users\Teodora\Desktop\7. semestar\Napredne baze podataka\projekat 1\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\_ViewImports.cshtml"
using NapredneBP_Project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Teodora\Desktop\7. semestar\Napredne baze podataka\projekat 1\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\_ViewImports.cshtml"
using NapredneBP_Project.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5fe33ee35c4132a2dfc332e0862d17bf993cbece", @"/Views/Movie/AllMovies.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67e24c10cb05e36984a1a36a31d216175c0bfda3", @"/Views/_ViewImports.cshtml")]
    public class Views_Movie_AllMovies : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Movie>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\Teodora\Desktop\7. semestar\Napredne baze podataka\projekat 1\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\Movie\AllMovies.cshtml"
   ViewData["Title"] = "All Movies";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>All Movies</h1>\r\n\r\n");
            WriteLiteral(@"
<table class=""table table-bordered table-striped"" style=""width:100%"">
    <thead>
        <tr>
            <th>Title</th>
            <th>Publishing date</th>
            <th>Description</th>
            <th>Rate</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 23 "C:\Users\Teodora\Desktop\7. semestar\Napredne baze podataka\projekat 1\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\Movie\AllMovies.cshtml"
         foreach (var obj in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td width=\"30%\">");
#nullable restore
#line 26 "C:\Users\Teodora\Desktop\7. semestar\Napredne baze podataka\projekat 1\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\Movie\AllMovies.cshtml"
                       Write(obj.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td width=\"30%\">");
#nullable restore
#line 27 "C:\Users\Teodora\Desktop\7. semestar\Napredne baze podataka\projekat 1\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\Movie\AllMovies.cshtml"
                       Write(obj.PublishingDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td width=\"100%\">");
#nullable restore
#line 28 "C:\Users\Teodora\Desktop\7. semestar\Napredne baze podataka\projekat 1\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\Movie\AllMovies.cshtml"
                        Write(obj.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td width=\"100%\">");
#nullable restore
#line 29 "C:\Users\Teodora\Desktop\7. semestar\Napredne baze podataka\projekat 1\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\Movie\AllMovies.cshtml"
                        Write(obj.Rate);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</td>
            <td width=""100%"">
                <div class=""rate"">
                    <input type=""radio"" id=""star1"" name=""rate"" value=""1"" />
                    <label for=""star1"" title=""text"">1 stars</label>
                    <input type=""radio"" id=""star2"" name=""rate"" value=""2"" />
                    <label for=""star2"" title=""text"">2 stars</label>
                    <input type=""radio"" id=""star3"" name=""rate"" value=""3"" />
                    <label for=""star3"" title=""text"">3 stars</label>
                    <input type=""radio"" id=""star4"" name=""rate"" value=""4"" />
                    <label for=""star4"" title=""text"">4 stars</label>
                    <input type=""radio"" id=""star5"" name=""rate"" value=""5"" />
                    <label for=""star5"" title=""text"">5 star</label>
                </div>
            </td>
        </tr>
");
#nullable restore
#line 45 "C:\Users\Teodora\Desktop\7. semestar\Napredne baze podataka\projekat 1\Baze-MVC\NapredneBP_Project\NapredneBP_Project\Views\Movie\AllMovies.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Movie>> Html { get; private set; }
    }
}
#pragma warning restore 1591
