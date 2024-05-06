
using Email.API.Domain.AggregatesModel.EmailAggregate.DTO;
using Email.API.Domain.AggregatesModel.EmailAggregate;
using System.Collections.Generic;
using Services.Core.BaseService;
using Email.API.Infrastructure;
using Services.Core.BaseRepository;
using FluentEmail.Core;
using Newtonsoft.Json;

namespace Email.API.Services
{
    public class EmailService: BaseService , IEmailService
    {
        private readonly IBaseRepository<EmailDBContext, EmailEntity> _emailRepository;
        private readonly IFluentEmail _fluentEmail;
        public EmailService(IBaseRepository<EmailDBContext, EmailEntity> emailRepository,
            IFluentEmail fluentEmail) {
            _emailRepository = emailRepository;
            _fluentEmail = fluentEmail;
        }

        public async Task<EmailEntity> SendEmail(EmailAddDto emailAddDto)
        {

            var email = Mapper.Map<EmailEntity>(emailAddDto);
            //email.Id = email.Id++;
            var result  = await _emailRepository.InsertAsync(email);
            try 
            {
                string jsonstring = "";
                if (result != null)
                {
                    jsonstring = JsonConvert.SerializeObject(result);
                }
                else {
                    jsonstring = JsonConvert.SerializeObject(emailAddDto);
                }
                await _fluentEmail.SetFrom("1520970162@qq.com")
                .To("1520970162@qq.com")
                .CC("1342173578@qq.com")
                .Subject("来自网站的咨询留言")
                .Body("来自网站的咨询留言:"+"\n"+ jsonstring)
                .SendAsync();
            }
            catch (Exception ex) { 
                Console.WriteLine(ex);
            }
            return  result ;
        }

    }
}
