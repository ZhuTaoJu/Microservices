using Email.API.Core;
using Services.Core;

namespace Email.API.Domain.AggregatesModel.BaseEntity
{
    public class ALLBase : IBaseKey<long> , ISoftDelete
    {
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public string  CreateUser { get; set; }
        public string  UpdateUser { get; set; }
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
