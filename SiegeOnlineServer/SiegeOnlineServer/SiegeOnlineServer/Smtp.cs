using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
// ReSharper disable InconsistentNaming

namespace SiegeOnlineServer
{
    public class Smtp
    {
        public string FromAddress { get; set; }

        public string SenderName { get; set; }

        public List<string> ToAddresses;

        public string Subject { get; set; }

        public string Body { get; set; }

        public string SmtpAccount { get; protected set; }

        public string SmtpPassword { get; protected set; }

        public int SmtpPort { get; protected set; }

        public string SmtpHost { get; protected set; }

        public Smtp(string from, string sender)
        {
            FromAddress = from;
            SenderName = sender;
            ToAddresses = new List<string>();
        }

        public void From(string from, string sender)
        {
            FromAddress = from;
            SenderName = sender;
        }

        public void To(string to)
        {
            if (!ToAddresses.Contains(to))
            {
                ToAddresses.Add(to);
            }
        }

        public void InitSmtpOfHotmail(string account, string password)
        {
            SmtpAccount = account;
            SmtpPassword = password;
            SmtpPort = 587;
            SmtpHost = "smtp.live.com";
        }

        public bool SendMailUseHotmail(string account = "", string password = "")
        {
            if (FromAddress.Length > 0 && SenderName.Length > 0)
            {
                InitSmtpOfHotmail(account, password);

                MailMessage msg = new MailMessage {From = new MailAddress(FromAddress, SenderName, Encoding.UTF8)};
                foreach (string address in ToAddresses)
                {
                    msg.To.Add(address);
                }
                msg.Subject = Subject;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.Body = Body;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = false;
                msg.Priority = MailPriority.High;

                SmtpClient client = new SmtpClient(SmtpHost, SmtpPort)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(SmtpAccount, SmtpPassword)
                };

                try
                {
                    client.Send(msg);
                    ServerPeer.Log.Debug("emails succeed");
                    return true;
                }
                catch (SmtpException ex)
                {
                    ServerPeer.Log.Debug("emails fail because of " + ex);
                    return false;
                }
            }
            return false;
        }
    }
}
