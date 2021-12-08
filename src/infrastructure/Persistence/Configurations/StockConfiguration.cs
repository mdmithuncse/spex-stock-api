using DataModel;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class StockConfiguration
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            // Table name and primarey key
            builder.Entity<StockModel>().ToTable("Stocks").HasKey(e => e.Id);
            builder.Entity<StockModel>().Property(e => e.Id).ValueGeneratedOnAdd();

            // Column types
            builder.Entity<StockModel>().Property(e => e.Sku).HasMaxLength(500).IsRequired();
            builder.Entity<StockModel>().Property(e => e.LocationId).IsRequired();
            builder.Entity<StockModel>().Property(e => e.Quantity).IsRequired();
        }
    }
}
