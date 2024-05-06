using Email.API.Domain.AggregatesModel.BaseEntity;

namespace Email.API.Domain.AggregatesModel.EmailAggregate
{
    public class EmailEntity : ALLBase
    {
        public string  CallName { get; set; }
        public string  Email    { get; set; }
        public string  Phone    { get; set; }
        public string  Description { get; set; }
        public string Remark { get; set; }
    }
}
