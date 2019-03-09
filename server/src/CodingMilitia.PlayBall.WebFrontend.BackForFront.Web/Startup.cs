using CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.Features.Groups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.AuthTokenHelpers;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;

[assembly: ApiController]

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices();


            services.AddSingleton<IDiscoveryCache>(r =>
            {
                var factory = r.GetRequiredService<IHttpClientFactory>();
                return new DiscoveryCache("https://localhost:5005", () => factory.CreateClient());
            });
            
            services
                .AddTransient<CustomCookieAuthenticationEvents>()
                .AddTransient<ITokenRefresher, TokenRefresher>()
                .AddTransient<AccessTokenHttpMessageHandler>()
                .TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services
                .AddHttpClient<GroupsController>((serviceProvider, client) =>
                {
                    // TODO: use serviceProvider to fetch the base address from configuration
                    client.BaseAddress = new Uri("http://localhost:5002");
                })
                .AddHttpMessageHandler<AccessTokenHttpMessageHandler>()
                .AddTransientHttpErrorPolicy(builder =>
                    builder.WaitAndRetryAsync(5, attempt => TimeSpan.FromSeconds(attempt)));

            services
                .AddHttpClient<ITokenRefresher,TokenRefresher>();
            
            
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies", options =>
                {
                    options.EventsType = typeof(CustomCookieAuthenticationEvents);
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";

                    options.Authority = "https://localhost:5005";
                    options.RequireHttpsMetadata = false;

                    options.ClientId = "WebFrontend";
                    options.ClientSecret = "secret";
                    options.ResponseType = OidcConstants.ResponseTypes.Code;

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.Scope.Add("GroupManagement");
                    options.Scope.Add(OidcConstants.StandardScopes.OfflineAccess);

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}