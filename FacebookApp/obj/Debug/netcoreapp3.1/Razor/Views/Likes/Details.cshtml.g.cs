#pragma checksum "D:\ITI\MVC\Project\NICKNAME\11\MVCProject\FacebookApp\Views\Likes\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b5683a56cf5a734e3f1fe560174dbffcdb3e9545"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Likes_Details), @"mvc.1.0.view", @"/Views/Likes/Details.cshtml")]
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
#line 1 "D:\ITI\MVC\Project\NICKNAME\11\MVCProject\FacebookApp\Views\_ViewImports.cshtml"
using FacebookApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\ITI\MVC\Project\NICKNAME\11\MVCProject\FacebookApp\Views\_ViewImports.cshtml"
using FacebookApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\ITI\MVC\Project\NICKNAME\11\MVCProject\FacebookApp\Views\Likes\Details.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5683a56cf5a734e3f1fe560174dbffcdb3e9545", @"/Views/Likes/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9ce7d7be762a95bf1d1f2de044e07ff6a15152ae", @"/Views/_ViewImports.cshtml")]
    public class Views_Likes_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<FacebookApp.Models.UserLikesPost>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "User", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Profile", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("color:black"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"
<div>
    <div class=""modal fade"" id=""likesModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""likesModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog"" role=""document"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    Likes
                </div>
                <div class=""modal-body"">
");
#nullable restore
#line 15 "D:\ITI\MVC\Project\NICKNAME\11\MVCProject\FacebookApp\Views\Likes\Details.cshtml"
                     if (Model != null)
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\ITI\MVC\Project\NICKNAME\11\MVCProject\FacebookApp\Views\Likes\Details.cshtml"
                         foreach (var like in Model)
                        {
                            if (like.IsLiked == true)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <dl class=\"row\">\r\n");
#nullable restore
#line 21 "D:\ITI\MVC\Project\NICKNAME\11\MVCProject\FacebookApp\Views\Likes\Details.cshtml"
                          
                            var u = await UserManager.FindByIdAsync(like.UserId);
                        

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <dt class=\"col-sm-2\">\r\n                            <img class=\"rounded-circle\" width=\"50\" height=\"50\"");
            BeginWriteAttribute("src", " src=\"", 1094, "\"", 1121, 1);
#nullable restore
#line 25 "D:\ITI\MVC\Project\NICKNAME\11\MVCProject\FacebookApp\Views\Likes\Details.cshtml"
WriteAttributeValue("", 1100, Url.Content(u.Image), 1100, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 1122, "\"", 1128, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        </dt>\r\n                        <dd class=\"col-sm-10\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5683a56cf5a734e3f1fe560174dbffcdb3e95456348", async() => {
                WriteLiteral("\r\n                                ");
#nullable restore
#line 29 "D:\ITI\MVC\Project\NICKNAME\11\MVCProject\FacebookApp\Views\Likes\Details.cshtml"
                           Write(u.Nickname);

#line default
#line hidden
#nullable disable
                WriteLiteral(" <br />\r\n                                ");
#nullable restore
#line 30 "D:\ITI\MVC\Project\NICKNAME\11\MVCProject\FacebookApp\Views\Likes\Details.cshtml"
                           Write(u.Email);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 28 "D:\ITI\MVC\Project\NICKNAME\11\MVCProject\FacebookApp\Views\Likes\Details.cshtml"
                                                                            WriteLiteral(like.UserId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                        </dd>\r\n                    </dl>\r\n");
#nullable restore
#line 35 "D:\ITI\MVC\Project\NICKNAME\11\MVCProject\FacebookApp\Views\Likes\Details.cshtml"
                            }
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n                <div class=\"modal-footer\">\r\n                    <button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">Close</button>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<User> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<User> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<FacebookApp.Models.UserLikesPost>> Html { get; private set; }
    }
}
#pragma warning restore 1591
