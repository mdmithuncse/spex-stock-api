using DataModel.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    public class SalesChannelModel : BaseIdAsInt
    {
        public string SalesChannelId { get; set; }

        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public LocationModel Location { get; set; }
    }
}
