using Microsoft.EntityFrameworkCore;
using PriceListsInfo.Models;

namespace PriceListsInfo.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<PriceListColumn> PriceListColumns { get; set; }
        public DbSet<PriceListColumnValue> PriceListColumnValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PriceList>()
            .HasMany(pl => pl.Columns)
            .WithOne(c => c.PriceList)
            .HasForeignKey(c => c.PriceListId);

            modelBuilder.Entity<PriceListColumn>()
                .HasMany(c => c.Values)
                .WithOne(v => v.PriceListColumn)
                .HasForeignKey(v => v.PriceListColumnId);
        }
    }
}
