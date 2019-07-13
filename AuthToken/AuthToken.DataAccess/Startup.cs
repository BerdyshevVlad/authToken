using AuthToken.DataAccess.Connections;
using AuthToken.DataAccess.Entities;
using AuthToken.DataAccess.Repositories;
using AuthToken.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthToken.DataAccess
{
    public static class Startup
    {

        public static void RegisterDependencies(string connectionString,IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbConnection>(options =>
            options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<ApplicationDbConnection>()
               .AddDefaultTokenProviders();


            services.AddScoped<IBooksRepository, BooksRepository>();
        }
    }
}
