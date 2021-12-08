using System;

namespace DomainModel.Base
{
    public class BaseAuditDto : IBaseAuditDto
    {
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
