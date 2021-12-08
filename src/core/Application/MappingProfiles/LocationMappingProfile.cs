using DataModel;
using DomainModel;

namespace Application.MappingProfiles
{
    public partial class AutoMapperProfile
    {
        public void CreateLocationProfile()
        {
            CreateMap<LocationModel, LocationResponse>().ReverseMap();
        }
    }
}
