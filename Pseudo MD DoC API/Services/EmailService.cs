using Pseudo_MD_DoC_API.Entities;
using Pseudo_MD_DoC_API.Models;
using Pseudo_MD_DoC_API.Persistence;
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
        //private EmailServerModel _emailServerModel;
        private AppDbContext _context;
        public EmailService(AppDbContext context)
        {
            //_emailServerModel = emailServerModel;
            _context = context;
        }

        public bool SendEmail(MailAddress from, string to, string subject, string message)
        {
            EmailServerModel esm = new EmailServerModel();
            try
            {
                //Get smtp config from database
                var config = from c in _context.Configuration where c.propertyName.StartsWith("emailServer") select c;
                esm.Server = config.Single(x => x.propertyName == "emailServer").propertyValue;
                esm.Port = Convert.ToInt32(config.Single(x => x.propertyName == "emailServerPort").propertyValue);
                esm.Username = config.Single(x => x.propertyName == "emailServerUsername").propertyValue;
                esm.Password = config.Single(x => x.propertyName == "emailServerPassword").propertyValue;
            }
            catch (Exception ex) { throw new Exception("Can't get SMTP server configuration"); }

            try
            {
                MailMessage mail = new MailMessage(from, new MailAddress(to));
                mail.Subject = subject;
                mail.Body = message;

                SmtpClient smtp = new SmtpClient(esm.Server, esm.Port);
                smtp.Credentials = new NetworkCredential(esm.Username, esm.Password);
                smtp.Send(mail);
            }
            catch(Exception ex) 
            {
                return false;
            }

            return true;
        }
    }
}
