using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BreakOutGame.Data;
using BreakOutGame.Data.Repositories;
using BreakOutGame.Filters;
using BreakOutGame.Models;
using BreakOutGame.Models.Domain;
using BreakOutGame.Models.Domain.RepsitoryInterfaces;
using BreakOutGame.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Principal;

namespace BreakOutGame
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

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options => {
                options.AddPolicy("teacher", policy => policy.RequireClaim(ClaimTypes.Role, "teacher"));
            });


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IBoBGroupRepository, BoBGroupRepository>();
            services.AddScoped<IBoBSessionRepository, BoBSessionRepository>();
            services.AddScoped<SessionNotAvailableFilter>();
           
            services.AddSession();

            services.AddScoped<AccountInit>();

            services.AddMvc();

            services.Configure<AuthMessageSenderOptions>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AccountInit init)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseSession();
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Session}/{action=Index}/{id?}");
            });
            //init.InitializeUsersAndCustomers().Wait();
        }
                
    }
}
