using System;
using System.Configuration;
using AutoMapper;
using Bnd.Api.Events;
using Bnd.Core.Automapper;
using Bnd.Core.Factory;
using Bnd.Core.Services.Cache;
using Bnd.Core.Services.Dealers;
using Bnd.Core.Services.Navigation;
using Bnd.Core.Services.PageConfiguration;
using Bnd.Core.Services.Redirects;
using Bnd.Core.Services.SiteConfiguration;
using Bnd.Core.Services.Sitemap;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SixLabors.ImageSharp.Web.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Extensions;
using UrlTracker.Web;

namespace Bnd.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="webHostEnvironment">The web hosting environment.</param>
        /// <param name="config">The configuration.</param>
        /// <remarks>
        /// Only a few services are possible to be injected here https://github.com/dotnet/aspnetcore/issues/9337
        /// </remarks>
        public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MediaProfiles());
                mc.AddProfile(new ConfigurationProfiles());
                mc.AddProfile(new NavigationProfiles());
                mc.AddProfile(new ComponentProfiles());
            });
            IMapper mapper = mapperConfig.CreateMapper();

#pragma warning disable IDE0022 // Use expression body for methods

            var umbracoBuilder = services.AddUmbraco(_env, _config)
                .AddBackOffice()
                .AddWebsite()
                .AddComposers();

            //adding support for Azure blob storage for storing media files for non-local environments
            if (!_env.IsDevelopment())
            {
                umbracoBuilder.AddAzureBlobMediaFileSystem();
            }

            umbracoBuilder.AddNotificationHandler<ContentSavingNotification, EditorModelEventHandler>()
                .Build();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
#pragma warning restore IDE0022 // Use expression body for methods
            services.AddSingleton(mapper);
            services.AddTransient<IComponentFactory, ComponentFactory>();
            services.AddTransient<INavigationFactory, NavigationFactory>();
            services.AddTransient<ISiteConfigurationService, SiteConfigurationService>();
            services.AddTransient<IPageConfigurationService, PageConfigurationService>();
            services.AddTransient<ISitemapService, SitemapService>();
            services.AddTransient<IPageRedirectsService, PageRedirectsService>();
            services.AddTransient<INavigationService, NavigationService>();
            services.AddTransient<IDealersService, DealersService>();
            services.AddTransient<ICacheService, CacheService>();

            //Configure HSTS
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });
           // services.AddImageSharp();
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The web hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseHsts();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
               
            }
            app.UseHttpsRedirection();
            //app.UseImageSharp();

            var nonVolatileDuration = Convert.ToInt32(_config.GetSection("Caching:NonVolatileDuration").Value);
            var volatileDuration = Convert.ToInt32(_config.GetSection("Caching:VolatileDuration").Value);

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    if (context.Context.Response.ContentType.Contains("image"))
                    {
                        context.Context.Response.Headers[HeaderNames.CacheControl]= $"public,max-age={nonVolatileDuration}";
                    } else
                    {
                        context.Context.Response.Headers[HeaderNames.CacheControl] = $"public,max-age={volatileDuration}";
                    }
                }
            });
            //app.UseXfo(options => options.SameOrigin());
            var securityPolicyDomain = _config.GetSection("Security:PolicyDomain").Value;
            var styleSrcPolicyDomains = _config.GetSection("Security:StyleSrcPolicy").Value;
            var scriptSrcPolicyDomains = _config.GetSection("Security:ScriptSrcPolicy").Value;
            var imgSrcPolicyDomains = _config.GetSection("Security:ImgSrcPolicy").Value;
            var frameSrcPolicyDomains = _config.GetSection("Security:FrameSrcPolicy").Value;
            var fontsSrcPolicyDomains = _config.GetSection("Security:FontsSrcPolicy").Value;
            var mediaSrcPolicyDomains = _config.GetSection("Security:MediaSrcPolicy").Value;
            var connectSrcPolicyDomains = _config.GetSection("Security:ConnectSrcPolicy").Value;

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
                context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
                context.Response.Headers.Add("permissions-policy", "accelerometer=(), autoplay=(), camera=(), cross-origin-isolated=(), display-capture=(), encrypted-media=(), fullscreen=(self), geolocation=(), gyroscope=(), keyboard-map=(), magnetometer=(), microphone=(), midi=(), payment=(), picture-in-picture=(), publickey-credentials-get=(), screen-wake-lock=(), sync-xhr=(), usb=(), web-share=(), xr-spatial-tracking=()");
                context.Response.Headers.Add("Content-Security-Policy", $"default-src 'self'; frame-ancestors {securityPolicyDomain}; style-src 'self' 'unsafe-inline' {styleSrcPolicyDomains}; script-src 'self' 'unsafe-inline' 'unsafe-eval' {scriptSrcPolicyDomains}; img-src * data:;connect-src {securityPolicyDomain} {connectSrcPolicyDomains}; font-src data: {securityPolicyDomain} {fontsSrcPolicyDomains}");

                await next();
            });

            app
                .UseUmbraco()
                .WithMiddleware(u =>
                {
                    u.UseBackOffice();
                    u.UseWebsite();
                    
                    //adding support for Azure blob storage for storing media files for non-local environments                   
                    u.UseUrlTracker();
                })
                .WithEndpoints(u =>
                {
                    u.UseInstallerEndpoints();
                    u.UseBackOfficeEndpoints();
                    u.UseWebsiteEndpoints();
                });
        }
    }
}
