using System;

namespace DataModel.Base
{
    public interface IBaseAudit
    {
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
