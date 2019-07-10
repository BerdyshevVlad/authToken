using AuthToken.Business.Services;
using AuthToken.Business.Services.Interfaces;
using AuthToken.DataAccess.Connections;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthToken.Business
{
    public static class Startup
    {
       
        public static void ConfigureServices(string connectionString, IServiceCollection services)
        {
            AuthToken.DataAccess.Startup.RegisterDependencies(connectionString, services);

            services.AddScoped<IBooksService, BooksService>();

        }
    }
}
