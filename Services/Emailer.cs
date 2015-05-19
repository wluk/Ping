using System.Net;
using System.Net.Mail;
using MailMessage = Services.DTO.MailMessage;

namespace Services
{
    public static class Emailer
    {
        //Wysłanie wiadomości technicznej
        public static void SendAllert(MailMessage mailmessage)
        {
            var mail = new System.Net.Mail.MailMessage();
            var smtpServer = new SmtpClient(mailmessage.SmtpClient);

            mail.From = new MailAddress(mailmessage.FromAddress);
            mail.To.Add(mailmessage.ToAddress);
            mail.Subject = mailmessage.Subject;
            mail.Body = mailmessage.Body;

            smtpServer.Port = mailmessage.Port;
            smtpServer.Credentials = new NetworkCredential(mailmessage.UserName, mailmessage.Password);
            smtpServer.EnableSsl = mailmessage.Ssl;

            
            //Wysłanie wiadomości do pliku
            smtpServer.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            smtpServer.PickupDirectoryLocation = @"C:\Hosts";
            //
            smtpServer.Send(mail);

            
        }
    }
}
