using AutoMapper;
using System.Collections;
using Email.API.Domain.AggregatesModel.EmailAggregate;
using Email.API.Domain.AggregatesModel.EmailAggregate.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Email.API.Services;

namespace Email.API.Controllers
{

    public class EmailController : AreaController
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<EmailEntity> SendEmail(EmailAddDto emailAddDto)
        {
            return await _emailService.SendEmail(emailAddDto);
        }
    }
}
