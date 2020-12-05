using System;
// using Contentful.Core;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Net.Http.Headers;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace blog.Api
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
            services.AddApplicationInsightsTelemetry();
            services.AddSwaggerDocument();
            services.AddHealthChecks();

            services.AddControllersWithViews();
            services.AddRazorPages();
            
            services.AddScoped(sp => new GraphQLHttpClient(
                new GraphQLHttpClientOptions {
                      EndPoint = new Uri($"https://graphql.contentful.com/content/v1/spaces/{_configuration["contentful:blog:spaceId"]}")
                },
                new NewtonsoftJsonSerializer(),
                new HttpClient
                {
                    DefaultRequestHeaders =
                    {
                        Accept = {
                            new MediaTypeWithQualityHeaderValue("application/json")
                        }, 
                        Authorization = new AuthenticationHeaderValue("Bearer", _configuration["contentful:blog:deliveryApiKey"])
                    }
                })
            );
            
            //services.AddSingleton(sp => new ContentfulClient(new HttpClient(), _configuration["contentful:blog:deliveryApiKey"], _configuration["contentful:blog:previewApiKey"], _configuration["contentful:blog:spaceId"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();     
            
            // Register Swagger generator and UI middleware
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapHealthChecks("/api/healthcheck", new HealthCheckOptions()
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
