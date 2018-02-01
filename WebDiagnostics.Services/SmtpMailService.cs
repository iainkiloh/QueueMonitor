using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using WebDiagnostics.Domain;
using WebDiagnostics.Interfaces;

namespace WebDiagnostics.Services
{
    public class SmtpMailService : IMailService
    {

        public bool Send(IEmailMessage item)
        {

            //System.Net.ServicePointManager.ServerCertificateValidationCallback =
            //    ((senderb, certificate, chain, sslPolicyErrors) => true);

            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential(Settings.SmtpUser, Settings.SmtpPassword);
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress(Settings.DefaultEmailFromAddress);

            // setup up the host, increase the timeout to 5 minutes
            smtpClient.Host = Settings.SmtpHost;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;
            smtpClient.Timeout = (60 * 5 * 1000);

            message.From = fromAddress;
            message.Subject = item.Subject;
            message.IsBodyHtml = false;
            message.Body = item.EmailBody;

            item.Recipients = new List<string> { Settings.DefaultEmailToAddress };
            if (item.Recipients != null)
            {
                foreach (var recip in item.Recipients)
                {
                    message.To.Add(recip);
                }
            }

            try
            {
                smtpClient.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                smtpClient.Dispose();
                message.Dispose();
            }

        }

    }
}
