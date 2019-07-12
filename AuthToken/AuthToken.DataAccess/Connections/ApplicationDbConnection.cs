using AuthToken.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthToken.DataAccess.Connections
{
    public class ApplicationDbConnection : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books { get; set; }


        public ApplicationDbConnection(DbContextOptions<ApplicationDbConnection> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
