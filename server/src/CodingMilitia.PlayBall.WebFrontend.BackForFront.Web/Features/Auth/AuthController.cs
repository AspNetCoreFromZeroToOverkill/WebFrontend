using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.Features.Auth
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAntiforgery _antiForgery;

        public AuthController(IAntiforgery antiForgery)
        {
            _antiForgery = antiForgery;
        }

        [HttpGet]
        [Route("info")]
        public ActionResult<AuthInfoModel> GetInfo()
        {
            var tokens = _antiForgery.GetAndStoreTokens(HttpContext);
            HttpContext.Response.Cookies.Append(
                "XSRF-TOKEN", 
                tokens.RequestToken, 
                new CookieOptions() {HttpOnly = false}); //allow JS to grab the cookie to put it in the request header
            
            return new AuthInfoModel
            {
                Name = User.FindFirst("name").Value
            };
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login([FromQuery] string returnUrl)
        {
            /*
             * No need to do anything here, as the auth middleware will take care of redirecting to IdentityServer4.
             * When the user is authenticated and gets back here, we can redirect to the desired url. 
             */
            return Redirect(string.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl);
        }
    }
}