using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BugTracker.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BugTracker.Extensions;
using Microsoft.AspNetCore.Http;
using BugTracker.Models;
using BugTracker.Models.Services;

namespace BugTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BugTrackerContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<BugTrackerUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //     .AddEntityFrameworkStores<BugTrackerContext>();
            services.AddIdentity<BugTrackerUser, IdentityRole>()// IdentityUser, BugTrackerUser
                 .AddEntityFrameworkStores<BugTrackerContext>()
                 .AddDefaultUI()
                 .AddDefaultTokenProviders();

            // For adding custom contexts
            services.AddScoped<IUserClaimsPrincipalFactory<BugTrackerUser>, AdditionalUserClaimsPrincipalFactory>();

            // Service Injection for FriendsList
            // Used for login partial
            services.AddTransient<UserFriendService>();

            // Service Injection for UserNotification
            // Used in login partial
            services.AddTransient<UserNotificationService>();

            // To access current user...
           // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews();
            services.AddControllers();
            services.AddRazorPages();

            // Add Google authentication
            /*
            services.AddAuthentication()
                .AddGoogle(options =>
                   {
                        IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");

                        options.ClientId = googleAuthNSection["ClientId"];
                        options.ClientSecret = googleAuthNSection["ClientSecret"];
                   });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               /* endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "project",
                    pattern: "{controller=Project}/{action=Index}/{id?}"
                    ); */
                //endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
