using DataModel.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    public class LockModel : BaseIdAsString
    {
        public string Sku { get; set; }
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public LocationModel Location { get; set; }
        public int Amount { get; set; }
        public string Reason { get; set; }
        public string TransactionId { get; set; }
        public string ReferenceId { get; set; }
    }
}
