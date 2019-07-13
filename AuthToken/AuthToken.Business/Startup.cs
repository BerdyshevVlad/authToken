using AuthToken.Business.Services;
using AuthToken.Business.Services.Interfaces;
using AuthToken.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AuthToken.Business
{
    public static class Startup
    {

        public static void ConfigureServices(string connectionString, IServiceCollection services)
        {
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IAccountService, AccountService>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            AuthToken.DataAccess.Startup.RegisterDependencies(connectionString, services);
        }

        public static void Ensure(IServiceProvider serviceProvider)
        {
            CreateRoles(serviceProvider).GetAwaiter().GetResult();
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleName = "admin";
            IdentityResult roleResult;

            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new ApplicationRole(roleName));
            }

            string email = "berdyshev1997@gmail.com";
            string adminPassword = "Qwe123!!";
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var admin = new ApplicationUser
                {

                    UserName = email,
                    Email = email,
                    FirstName = "Super",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var createPowerUser = await userManager.CreateAsync(admin, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }

            }


        }
    }
}
