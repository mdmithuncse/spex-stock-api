using DataModel;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations
{
    public class LocationConfiguration
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            // Table name and primarey key
            builder.Entity<LocationModel>().ToTable("Locations").HasKey(e => e.Id);
            builder.Entity<LocationModel>().Property(e => e.Id).ValueGeneratedOnAdd();

            // Column types
            builder.Entity<LocationModel>().Property(e => e.Location).HasMaxLength(300).IsRequired();
        }
    }
}
