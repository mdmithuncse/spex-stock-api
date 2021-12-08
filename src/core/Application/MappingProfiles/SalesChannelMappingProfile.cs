using DataModel;
using DomainModel;

namespace Application.MappingProfiles
{
    public partial class AutoMapperProfile
    {
        public void CreateSalesChannelProfile()
        {
            CreateMap<SalesChannelModel, SalesChannelResponse>().ReverseMap();
        }
    }
}
