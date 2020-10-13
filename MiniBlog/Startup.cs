using AutoMapper;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiniBlog.Core.Constants;
using MiniBlog.Core.Interfaces;
using MiniBlog.Core.Interfaces.Repositories;
using MiniBlog.Core.Models;
using MiniBlog.Core.Services;
using MiniBlog.Data;
using MiniBlog.Data.InMemory;
using MiniBlog.IdentityServer;
using MiniBlog.IdentityServer.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MiniBlog
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
            services.AddDbContext<MiniBlogDBContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MiniBlogDBContext>();

            services.AddIdentityServer(options => options.Events.RaiseFailureEvents = true)
                .AddApiAuthorization<ApplicationUser, MiniBlogDBContext>();

            services.AddAuthorization(options=> 
            { 
                options.AddPolicy(Policies.RequireMinimumRole, policy => policy.Requirements.Add(new MinimumRoleRequirement(Roles.Standard))); 
            });

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
                .AddIdentityServerJwt();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
            });

            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddTransient<IProfileService, ProfileService>();
            services.AddSingleton<IAuthorizationHandler, MinimumRoleHandler>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            RegisterRepositories(services);
            RegisterServices(services);
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            //app.UseAuthentication(); not needed, since UseIdentityServer adds the authentication middleware
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ICommentService, CommentService>();
            services.AddSingleton<IBlogPostService, BlogPostService>();
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddSingleton<ICommentRepository, InMemoryCommentRepository>();
            services.AddSingleton<IBlogRepository, InMemoryBlogPostRepository>();
        }
    }
}