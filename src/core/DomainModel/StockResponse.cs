using DomainModel.Base;

namespace DomainModel
{
    public class StockResponse : BaseIdAsIntDto
    {
        public string Sku { get; set; }
        public int LocationId { get; set; }
        public int Quantity { get; set; }
    }
}
