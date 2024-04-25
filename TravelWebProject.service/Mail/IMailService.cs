using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace TravelWebProject.service.Mail
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}