using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MiniBlog.Core.Constants;
using MiniBlog.Core.Entities;
using MiniBlog.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Data
{
    public static class SeedData
    {
        public const string AdminUserName = "Admin";
        public const string AdminEmail = "admin@contoso.com";
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using var context = new MiniBlogDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<MiniBlogDBContext>>(),
                serviceProvider.GetRequiredService<IOptions<OperationalStoreOptions>>()
                );
            await CreateRoles(serviceProvider);
            if (!context.Users.Any())
            {
                var adminID = await EnsureUser(serviceProvider, testUserPw, AdminUserName);
                await EnsureRole(serviceProvider, adminID, Roles.Admin);
            }
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            bool adminRoleExists = await RoleManager.RoleExistsAsync(Roles.Admin);
            bool standardRoleExists = await RoleManager.RoleExistsAsync(Roles.Standard);

            if (!adminRoleExists)
            {
                await RoleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
            if (!standardRoleExists)
            {
                await RoleManager.CreateAsync(new IdentityRole(Roles.Standard));
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = UserName,
                    Email = AdminEmail,
                    EmailConfirmed = true,
                    ProfilePicture = new Image
                    {
                        Id = Guid.NewGuid(),
                        ImageName = "SeedUserAvatar",
                        ImagePath = new Uri("img/SeedUserAvatar.png", UriKind.Relative)
                    }
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}