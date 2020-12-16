using Microsoft.EntityFrameworkCore;


namespace McSharesAPI.Models
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
    }
}