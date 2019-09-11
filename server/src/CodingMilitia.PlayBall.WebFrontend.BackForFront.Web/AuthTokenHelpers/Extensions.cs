using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.AuthTokenHelpers
{
    public static class Extensions
    {
        public static Task<string> GetAccessTokenAsync(this HttpContext context)
            => context.GetTokenAsync("access_token");
    }
}