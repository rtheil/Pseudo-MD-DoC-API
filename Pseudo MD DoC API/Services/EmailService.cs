using Pseudo_MD_DoC_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Pseudo_MD_DoC_API.Services
{
    public interface IEmailService
    {
        bool SendEmail(MailAddress From, string to, string subject, string message);
    }
    public class EmailService : IEmailService
    {
        EmailServerModel _emailServerModel;
        public EmailService(EmailServerModel emailServerModel)
        {
            _emailServerModel = emailServerModel;
        }

        public bool SendEmail(MailAddress from, string to, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage(from, new MailAddress(to));
                mail.Subject = subject;
                mail.Body = message;

                SmtpClient smtp = new SmtpClient(_emailServerModel.Server, _emailServerModel.Port);
                smtp.Credentials = new NetworkCredential(_emailServerModel.Username, _emailServerModel.Password);
                smtp.Send(mail);
            }
            catch(Exception ex) {
                return false;
            }

            return false;
        }
    }
}
