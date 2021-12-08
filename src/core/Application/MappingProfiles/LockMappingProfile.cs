using DataModel;
using DomainModel;

namespace Application.MappingProfiles
{
    public partial class AutoMapperProfile
    {
        public void CreateLockProfile()
        {
            CreateMap<LockModel, LockResponse>().ReverseMap();
        }
    }
}
