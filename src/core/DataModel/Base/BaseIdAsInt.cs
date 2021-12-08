namespace DataModel.Base
{
    public class BaseIdAsInt : BaseAudit, IBaseIdAsInt
    {
        public int Id { get; set; }
    }
}
