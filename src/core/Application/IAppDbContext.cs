using DataModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application
{
    public interface IAppDbContext
    {
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<LockModel> Locks { get; set; }
        public DbSet<SalesChannelModel> SalesChannels { get; set; }
        public DbSet<StockModel> Stocks { get; set; }

        public Task<int> SaveChangesAsync();
    }
}
