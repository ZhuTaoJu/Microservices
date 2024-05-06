
using Email.API.Domain.AggregatesModel.EmailAggregate;
using Email.API.Infrastructure;
using Services.Core.BaseRepository;

namespace Email.API.Repositories
{
    public class EmailRepository : BaseRepository<EmailDBContext, EmailEntity, long>, IEmailRepository
    {
        public EmailRepository(EmailDBContext context) : base(context)
        {

        }
    }
}
