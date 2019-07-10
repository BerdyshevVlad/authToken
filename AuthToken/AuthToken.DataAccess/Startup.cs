using AuthToken.DataAccess.Connections;
using AuthToken.DataAccess.Repositories;
using AuthToken.DataAccess.Repositories.Interfaces;
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

            services.AddScoped<IBooksRepository, BooksRepository>();
        }
    }
}
