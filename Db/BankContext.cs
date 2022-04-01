using Microsoft.EntityFrameworkCore;
using MyBank.Models;

namespace MyBank.Db
{
    public class BankContext : DbContext
    {
        public DbSet<VipClient> Vips { get; set; } = null!;
        public DbSet<CommonClient> Commons { get; set; } = null!;
        public DbSet<Bill> Bills { get; set; } = null!;

        public BankContext()
        {
            Database.EnsureCreated();
            Vips.Load();
            Commons.Load();
            Bills.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=bankdb;Trusted_Connection=True;");
        }
    }
}

