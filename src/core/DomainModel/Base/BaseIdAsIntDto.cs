namespace DomainModel.Base
{
    public class BaseIdAsIntDto : BaseAuditDto, IBaseIdAsIntDto
    {
        public int Id { get; set; }
    }
}
