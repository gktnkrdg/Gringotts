using GringottsBank.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace GringottsBank.Data
{
    public class GringottsBankDbContext : DbContext
    {
        public GringottsBankDbContext(DbContextOptions<GringottsBankDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GringottsBankDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Account>(b =>
            {
                b.HasOne(c => c.Customer).WithMany(t=>t.Accounts).HasForeignKey(c => c.CustomerID);

            });
            
                 
            modelBuilder.Entity<Transaction>(b =>
            {
                b.HasOne(c => c.Account).WithMany(c => c.Transactions);
                b.Property(c => c.TransactionType).HasConversion<short>();
            });
        }
    }
}