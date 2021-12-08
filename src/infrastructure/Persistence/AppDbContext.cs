using Application;
using DataModel;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using System.Threading.Tasks;

namespace Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<LockModel> Locks { get; set; }
        public DbSet<SalesChannelModel> SalesChannels { get; set; }
        public DbSet<StockModel> Stocks { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            LocationConfiguration.OnModelCreating(builder);
            LockConfiguration.OnModelCreating(builder);
            SalesChannelConfiguration.OnModelCreating(builder);
            StockConfiguration.OnModelCreating(builder);
        }
    }
}
