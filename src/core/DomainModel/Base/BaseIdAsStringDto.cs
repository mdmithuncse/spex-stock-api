namespace DomainModel.Base
{
    public class BaseIdAsStringDto : BaseAuditDto, IBaseIdAsStringDto
    {
        public string Id { get; set; }
    }
}
