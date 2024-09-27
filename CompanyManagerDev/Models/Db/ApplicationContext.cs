using Microsoft.EntityFrameworkCore;

namespace CompanyManagerDev.Models.Db
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Company> Company { get; set; } = null!;
        public DbSet<Role> Role { get; set; } = null!;
        public DbSet<Project> Project { get; set; } = null!;
        public DbSet<UsersCompanies> UsersCompanies { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
