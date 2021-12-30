using cuenta_ahorro.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace cuenta_ahorro.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<ManagementAccount> ManagementAccount { get; set; }
        public DbSet<OpeningSavingAccount> OpeningSavingAccount { get; set;}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
    }
}
