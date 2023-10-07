using Microsoft.EntityFrameworkCore;
using TestCode.Models.DomainModels;

namespace TestCode.DataAccess
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        public DbSet<Items> Items { get; set; }
    }
}
