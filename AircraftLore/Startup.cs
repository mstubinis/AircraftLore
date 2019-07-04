using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AircraftLore.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AircraftLore{
    public class Startup{

        public IConfiguration Configuration { get; }


        public Startup(IHostingEnvironment env, IConfiguration configuration, ILoggerFactory loggerFactory){
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services){
            services.Configure<CookiePolicyOptions>(options => { // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded    = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //start up database for aircraft
            services.AddDbContext<AircraftDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AircraftLoreContext")));

            //start up identity
            services.AddDbContext<IdentityDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityContextConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<IdentityDatabaseContext>();


            services.AddSingleton<IEmailSender, Data.EmailSender>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env){
            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
            }else{
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
            var options = new WebSocketOptions{
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4096
            };
            app.UseWebSockets(options);
        }
    }
}
