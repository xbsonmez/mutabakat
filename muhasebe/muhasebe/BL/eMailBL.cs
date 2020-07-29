namespace BL
{
    using System;
    using System.Net;
    using System.Net.Mail;

    public class eMailBL
    {
        public bool informationSend(string to, string subject, string mailBody)
        {
            try
            {
                MailMessage message = new MailMessage {
                    IsBodyHtml = true,
                    Body = mailBody
                };
                message.To.Add(to);
                message.From = new MailAddress("mutabakat@adalicammuhasebe.com");
                message.Subject = subject;
                new SmtpClient("mail.adalicammuhasebe.com") { 
                    EnableSsl = false,
                    Port = 0x24b,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("mutabakat@adalicammuhasebe.com", "adaliM2014"),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                }.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool send(string to, string subject, string mailBody, string from, string senderMail, string senderPass)
        {
            try
            {
                MailMessage message = new MailMessage {
                    IsBodyHtml = true,
                    Body = mailBody
                };
                message.To.Add(to);
                message.From = new MailAddress(from);
                message.Subject = subject;
                new SmtpClient("smtp.yandex.com.tr", 0x24b) { 
                    Credentials = new NetworkCredential(senderMail, senderPass),
                    EnableSsl = true
                }.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

