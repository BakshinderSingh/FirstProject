using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProjectApp.Entities;
using CourseProjectApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseProjectApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddDbContext<ProfileContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ProfileContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddFacebook(o => { o.AppId = Configuration["Authentication:Facebook:Id"]; o.AppSecret = Configuration["Authentication:Facebook:AppSecret"]; });
            services.AddMvc();
            services.Configure<MessageSenderOptions>(Configuration);
            services.AddTransient<ISmsSend, MessageSend>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ProfileContext context)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Main/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            Initializer.InitializeContext(context);
            //app.UseFacebookAuthentication();
            app.UseMvc(ConfigurationRoute);
            
            app.Run(async (context1) =>
            {
                await context1.Response.WriteAsync("Hello World!");
            });
        }

        private void ConfigurationRoute(IRouteBuilder obj)
        {
            obj.MapRoute("Default", "{controller=Main}/{action=Index}/{id?}");
        }
    }
}
