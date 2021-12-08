using DataModel;
using DomainModel;

namespace Application.MappingProfiles
{
    public partial class AutoMapperProfile
    {
        public void CreateStockProfile()
        {
            CreateMap<StockModel, StockResponse>().ReverseMap();
            CreateMap<StockModel, StockRequest>().ReverseMap();
        }
    }
}
