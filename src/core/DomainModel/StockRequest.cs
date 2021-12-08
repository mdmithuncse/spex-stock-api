using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class StockRequest
    {
        [Required]
        public string Sku { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
