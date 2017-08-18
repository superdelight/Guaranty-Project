using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PonziBussinessLogic.Utitlity
{
    public class EmailUtility
    {

        public string RecepientEmail { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }
        public string EmailSource { get; set; }
        public string HostAddress { get; set; }
        public bool SSLEnabled { get; set; }
        public string Password { get; set; }
        public int HostPort { get; set; }
        public bool SendEmail(bool IsHTMLBody)
        {
            bool ok = false;
            try
            {

                using (MailMessage mailMessage = new MailMessage())
                {

                    mailMessage.From = new MailAddress(EmailSource);
                    mailMessage.Subject = Subject;
                    mailMessage.Body = EmailBody;
                    mailMessage.IsBodyHtml = IsHTMLBody;
                    mailMessage.To.Add(new MailAddress(RecepientEmail));

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = HostAddress;
                    //smtp.EnableSsl = true;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = EmailSource;
                    NetworkCred.Password = "Tolulope890@";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = HostPort;


                    smtp.Send(mailMessage);
                    smtp.Timeout = 900000000;
                    ok = true;




                }
            }
            catch
            {
                ok = false;
            }
            return ok;
        }
        public bool SendMail(bool IsHTMLBody, byte[] attachment, string docDescription)
        {
            bool ok = false;
            try
            {

                using (MailMessage mailMessage = new MailMessage())
                {

                    mailMessage.From = new MailAddress(EmailSource);
                    mailMessage.Subject = Subject;
                    mailMessage.Body = EmailBody;
                    mailMessage.IsBodyHtml = IsHTMLBody;
                    mailMessage.To.Add(new MailAddress(RecepientEmail));

                    MemoryStream m = new MemoryStream(attachment);
                    Attachment firstAttachment = new Attachment(m, string.Format("{0}.pdf", docDescription));

                    mailMessage.Attachments.Add(firstAttachment);

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = HostAddress;
                    smtp.EnableSsl = SSLEnabled;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = EmailSource;
                    NetworkCred.Password = Password;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = HostPort;
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

