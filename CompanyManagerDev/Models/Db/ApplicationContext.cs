using Microsoft.EntityFrameworkCore;

namespace CompanyManagerDev.Models.Db
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UsersCompanies> UsersCompanies { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
