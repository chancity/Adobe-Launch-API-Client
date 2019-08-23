using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using AdobeLaunch.ApiProxy.Services;
using AdobeLaunch.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AdobeLaunch.ApiProxy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private static SecurityKey GetSecurityKey(string filePath)
        {
            var certificate = new X509Certificate2(X509Certificate.CreateFromSignedFile(filePath));
            var rsaPrivateKey = certificate.GetRSAPrivateKey();
            return new RsaSecurityKey(rsaPrivateKey);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });


            var accountOptions = new AccountOptions(
                Configuration[Defaults.ORGANIZATION_ID],
                Configuration[Defaults.TECHNICAL_ACCOUNT_ID],
                Configuration[Defaults.CLIENT_ID],
                Configuration[Defaults.CLIENT_SECRET],
                GetSecurityKey(Configuration[Defaults.CERTIFICATE_PATH]));

            var reactorApi = new ReactorApi(accountOptions);
            //    
            //    , null, new HttpClientHandler()
            //{
            //    Proxy = new WebProxy("http://localhost:8888"),
            //    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.None
            //});


            services
                .AddSingleton(reactorApi)
                .AddSingleton<ReactorService>();


            services.AddCors(options =>
            {
                options.AddPolicy(Defaults.ALL_CORS_POLICY,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            app.UseCors(Defaults.ALL_CORS_POLICY);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                );

                routes.MapRoute(
                    name: "proxy",
                    template: "adobe_launch/proxy/{*slug}",
                    defaults: new { controller = "AdobeLaunch", action = "CatchAll" }
                );

            });
        }
    }
}