using GringottsBank.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace GringottsBank.Data
{
    public class GringottsBankDbContext : DbContext
    {
        public GringottsBankDbContext(DbContextOptions<GringottsBankDbContext> options) : base(options)
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GringottsBankDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BankAccount>(b =>
            {
                b.HasOne(c => c.Customer).WithMany(t => t.Accounts).HasForeignKey(c => c.CustomerId);
                b.Property(e => e.ConcurrencyStamp)
                    .HasColumnName("xmin")
                    .HasColumnType("xid")
                    .ValueGeneratedOnAddOrUpdate()
                    .IsConcurrencyToken();
            });


            modelBuilder.Entity<Transaction>(b =>
            {
                b.HasOne(c => c.BankAccount).WithMany(c => c.Transactions);
                b.Property(c => c.TransactionType).HasConversion<short>();
            });
        }
    }
}