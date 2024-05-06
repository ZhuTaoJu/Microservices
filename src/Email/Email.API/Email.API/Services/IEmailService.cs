using Email.API.Domain.AggregatesModel.EmailAggregate.DTO;
using Email.API.Domain.AggregatesModel.EmailAggregate;

namespace Email.API.Services
{
    public partial interface IEmailService
    {
        Task<EmailEntity> SendEmail(EmailAddDto emailAddDto);
    }
}
