using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.Antiforgery
{
    public class ValidateAntiForgeryTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAntiforgery _antiforgery;

        public ValidateAntiForgeryTokenMiddleware(RequestDelegate next, IAntiforgery antiforgery)
        {
            _next = next;
            _antiforgery = antiforgery;
        }

        public async Task Invoke(HttpContext context)
        {
            if (ShouldValidate(context))
            {
                await _antiforgery.ValidateRequestAsync(context);
            }

            await _next(context);
        }

        private static bool ShouldValidate(HttpContext context)
        {
            // as seen on https://github.com/aspnet/AspNetCore/blob/release/3.0/src/Mvc/Mvc.ViewFeatures/src/Filters/AutoValidateAntiforgeryTokenAuthorizationFilter.cs
            
            var method = context.Request.Method;
            return !(HttpMethods.IsGet(method)
                     || HttpMethods.IsHead(method)
                     || HttpMethods.IsTrace(method)
                     || HttpMethods.IsOptions(method));
        }
    }
}