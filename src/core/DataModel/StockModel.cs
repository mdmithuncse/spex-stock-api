using DataModel.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    public class StockModel : BaseIdAsInt
    {
        public string Sku { get; set; }

        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public LocationModel Location { get; set; }  
        
        public int Quantity { get; set; }
    }
}
