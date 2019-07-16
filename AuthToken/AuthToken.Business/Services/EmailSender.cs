using AuthToken.Business.Services.Interfaces;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AuthToken.Business.Services
{
    public class EmailSender : IEmailSender
    {

        public bool SendMail(string email, string subject,string body)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("berdyshev1997@gmail.com", "TokenAuthApp");
                mailMessage.To.Add(email);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                smtpClient.Port = 25;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential("berdyshev1997@gmail.com", "1997slvs");
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 10000;

                smtpClient.SendMailAsync(mailMessage).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                //ignore
                return false;
            }

            return true;
        }
    }
}
