using apiForReal.models;
using Microsoft.EntityFrameworkCore;

namespace apiForReal.admin.api
{
    public class DbTicketsContext : DbContext
    {
        public DbTicketsContext(DbContextOptions<DbTicketsContext> options) : base(options)
        {
            
        }
        
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Worker> Worker { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }

        
    }
}