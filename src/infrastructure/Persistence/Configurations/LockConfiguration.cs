using DataModel;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class LockConfiguration
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            // Table name and primarey key
            builder.Entity<LockModel>().ToTable("Locks").HasKey(e => e.Id);

            // Column types
            builder.Entity<LockModel>().Property(e => e.Sku).HasMaxLength(500).IsRequired();
            builder.Entity<LockModel>().Property(e => e.LocationId).IsRequired();
            builder.Entity<LockModel>().Property(e => e.Amount).IsRequired();
            builder.Entity<LockModel>().Property(e => e.Reason).HasMaxLength(50).IsRequired();
            builder.Entity<LockModel>().Property(e => e.TransactionId).HasMaxLength(36).IsRequired();
            builder.Entity<LockModel>().Property(e => e.ReferenceId).HasMaxLength(36).IsRequired();
        }
    }
}
