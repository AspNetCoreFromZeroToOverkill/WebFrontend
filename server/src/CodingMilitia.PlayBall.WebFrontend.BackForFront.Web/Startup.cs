using System.Collections.Generic;
using CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.AuthTokenHelpers;
using CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.Configuration;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using System.IO;
using System.Net;
using CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.ApiRouting;
using CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.Security;
using ProxyKit;

[assembly: ApiController]

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices();


            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                return configuration.GetSection<AuthServiceSettings>("AuthServiceSettings");
            });

            services.AddSingleton<IDiscoveryCache>(serviceProvider =>
            {
                var authServiceConfig = serviceProvider.GetRequiredService<AuthServiceSettings>();
                var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                return new DiscoveryCache(authServiceConfig.Authority, () => factory.CreateClient());
            });

            services
                .AddTransient<CustomCookieAuthenticationEvents>()
                .AddTransient<ITokenRefresher, TokenRefresher>()
                .AddTransient<AccessTokenHttpMessageHandler>()
                .AddHttpContextAccessor();

            services
                .AddHttpClient<ITokenRefresher, TokenRefresher>();


            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies", options => { options.EventsType = typeof(CustomCookieAuthenticationEvents); })
                .AddOpenIdConnect("oidc", options =>
                {
                    var authServiceConfig = _configuration.GetSection<AuthServiceSettings>("AuthServiceSettings");

                    options.SignInScheme = "Cookies";

                    options.Authority = authServiceConfig.Authority;
                    options.RequireHttpsMetadata = authServiceConfig.RequireHttpsMetadata;

                    options.ClientId = authServiceConfig.ClientId;
                    options.ClientSecret = authServiceConfig.ClientSecret;
                    options.ResponseType = OidcConstants.ResponseTypes.Code;

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.Scope.Add("GroupManagement");
                    options.Scope.Add(OidcConstants.StandardScopes.OfflineAccess);

                    options.CallbackPath = "/auth/signin-oidc";

                    options.Events.OnRedirectToIdentityProvider = context =>
                    {
                        if (!context.HttpContext.Request.Path.StartsWithSegments("/auth/login"))
                        {
                            context.HttpContext.Response.StatusCode = 401;
                            context.HandleResponse();
                        }

                        return Task.CompletedTask;
                    };
                });

            services
                .AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            var dataProtectionKeysLocation =
                _configuration.GetSection<DataProtectionSettings>(nameof(DataProtectionSettings))?.Location;
            if (!string.IsNullOrWhiteSpace(dataProtectionKeysLocation))
            {
                services
                    .AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo(dataProtectionKeysLocation));
                // TODO: encrypt the keys
            }

            services.AddProxy();
            
            services.AddSingleton(
                new ProxiedApiRouteEndpointLookup(
                    _configuration.GetSection<Dictionary<string, string>>("ApiRoutes")));

            services
                .AddSingleton<EnforceAuthenticatedUserMiddleware>()
                .AddSingleton<ValidateAntiForgeryTokenMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMiddleware<EnforceAuthenticatedUserMiddleware>();
            app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();

            app.UseMvc();
            
            app.Map("/api", api =>
            {
                api.RunProxy(async context =>
                {
                    var endpointLookup = context.RequestServices.GetRequiredService<ProxiedApiRouteEndpointLookup>();
                    if (endpointLookup.TryGet(context.Request.Path, out var endpoint))
                    {
                        var forwardContext = context
                            .ForwardTo(endpoint)
                            .CopyXForwardedHeaders();

                        var token = await context.GetAccessTokenAsync();
                        forwardContext.UpstreamRequest.SetBearerToken(token);

                        return await forwardContext.Send();
                    }

                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                });
            });
        }
    }
}