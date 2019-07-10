using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(string connectionString, IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbConnection>(options =>
            new DatabaseConnection(connectionString));

            Cazamio.Services.Startup.Configure(appSettings["DbConnectionString"], services);

        }
    }
}
