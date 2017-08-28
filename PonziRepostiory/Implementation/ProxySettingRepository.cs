using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class ProxySettingRepository : Repository<ProxySetting>, IProxyRepository
    {
        private NobleDBEntities Context;
        public ProxySettingRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }

        public ProxySetting GetActiveProxySetting()
        {
            return Context.ProxySettings.Where(c => c.IsActive == true).FirstOrDefault();
        }

        public bool SendEmail(bool IsHTMLBody, string message, string header,string recepientEmail)
        {
            bool ok = false;
            try
            {
                var activeEmail = Context.ProxySettings.Where(c => c.IsActive == true).FirstOrDefault();
    
                using (MailMessage mailMessage = new MailMessage())
                {

                    mailMessage.From = new MailAddress(activeEmail.EmailSource);
                    mailMessage.Subject = header;
                    mailMessage.Body = message;
                    mailMessage.IsBodyHtml = IsHTMLBody;
                    mailMessage.To.Add(new MailAddress(recepientEmail));
                    mailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    mailMessage.HeadersEncoding = System.Text.Encoding.GetEncoding("utf-8");

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = activeEmail.SMTPServer;
                    smtp.EnableSsl = true;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = activeEmail.EmailSource;
                    NetworkCred.Password = activeEmail.Password;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = Convert.ToInt32(activeEmail.PortNumber);
                    smtp.Send(mailMessage);
                    smtp.Timeout = 90000000;
                    ok = true;




                }
            }
            catch (Exception ex)
            {
                ok = false;
            }
            return ok;
        }

        public bool SendEmail(bool IsHTMLBody, byte[] attachment, string docDescription, string message, string header, string destinationEmail)
        {
            bool ok = false;
            try
            {
                var activeEmail = Context.ProxySettings.Where(c => c.IsActive == true).FirstOrDefault();
                using (MailMessage mailMessage = new MailMessage())
                {

                    mailMessage.From = new MailAddress(activeEmail.EmailSource);
                    mailMessage.Subject = header;
                    mailMessage.Body = message;
                    mailMessage.IsBodyHtml = IsHTMLBody;
                    mailMessage.To.Add(new MailAddress(destinationEmail));

                    MemoryStream m = new MemoryStream(attachment);
                    Attachment firstAttachment = new Attachment(m, string.Format("{0}.pdf", docDescription));

                    mailMessage.Attachments.Add(firstAttachment);

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = activeEmail.SMTPServer;
                    smtp.EnableSsl = true;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = activeEmail.EmailSource;
                    NetworkCred.Password = activeEmail.Password;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port =Convert.ToInt32(activeEmail.PortNumber);
                    smtp.Send(mailMessage);
                    smtp.Timeout = 90000000;
                    ok = true;
                }
            }
            catch
            {
                ok = false;
            }
            return ok;
        }
    }
}
