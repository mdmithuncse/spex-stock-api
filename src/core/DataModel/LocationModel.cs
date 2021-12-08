using DataModel.Base;
using System.Collections.Generic;

namespace DataModel
{
    public class LocationModel : BaseIdAsInt
    {
        public string Location { get; set; }

        public ICollection<StockModel> Stocks { get; set; }

        public ICollection<SalesChannelModel> SalesChannels { get; set; }
    }
}
