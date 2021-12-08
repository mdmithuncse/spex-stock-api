using AutoMapper;

namespace Application.MappingProfiles
{
    public partial class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateLocationProfile();
            CreateLockProfile();
            CreateStockProfile();
            CreateSalesChannelProfile();
        }
    }
}
