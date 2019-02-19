using CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.Features.Groups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;

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
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices();

            services
                .AddHttpClient<GroupsController>((serviceProvider, client) => 
                {
                    // TODO: use serviceProvider to fetch the base address from configuration
                    client.BaseAddress = new Uri("http://localhost:5002");
                }).AddPolicyHandler(
                    HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(5, attempt => TimeSpan.FromSeconds(attempt)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
