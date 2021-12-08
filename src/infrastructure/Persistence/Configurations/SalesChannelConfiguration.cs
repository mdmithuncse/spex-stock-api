using DataModel;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class SalesChannelConfiguration
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            // Table name and primarey key
            builder.Entity<SalesChannelModel>().ToTable("SalesChannels").HasKey(e => e.Id);
            builder.Entity<SalesChannelModel>().Property(e => e.Id).ValueGeneratedOnAdd();

            // Column types
            builder.Entity<SalesChannelModel>().Property(e => e.SalesChannelId).HasMaxLength(36).IsRequired();
            builder.Entity<SalesChannelModel>().Property(e => e.LocationId).IsRequired();
        }
    }
}
