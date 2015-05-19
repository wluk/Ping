using System;
namespace Services.DTO
{
    public class MailMessage
    {
        //mail
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        //SmtpServer
        public string SmtpClient { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public MailMessage(string addressHost, string adminEmail, string nameHost, int errorCode, string errorDetail)
        {
            FromAddress = "jcommerce@jcommerc.pl";
            Subject = "Alarm w aplikacji: " + nameHost;
            Body = String.Format("Aplikacja {0} o adresie {1}, działa niepoprawnie od {2}.\n\n{3}\n\nKod błędu: {4}", nameHost, addressHost, DateTime.Now, errorDetail, errorCode);
            ToAddress = adminEmail;

            SmtpClient = "smtp.google.com";
            Port = 25;
            Ssl = false;
            UserName = "SystemName";
            Password = "SystemPassword";
        }
    }
}
