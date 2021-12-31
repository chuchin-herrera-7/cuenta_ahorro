using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace cuenta_ahorro.Models
{
    public class Email_
    {
        private string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string[] To { get; set; }
        public string[] Cc { get; set; }
        private string[] Bcc { get; set; }

        private readonly SmtpClient SmtpClient;
        private readonly IConfiguration Smtp;
        private MailMessage MailMessage;

        public Email_(IConfiguration Configuration)
        {
            bool ModeProduction = Configuration.GetSection("IsProduction").Value == "true" ? true : false;
            this.Smtp = ModeProduction ? Configuration.GetSection("Smtp") : Configuration.GetSection("SmtpTest");

            this.SmtpClient             = new SmtpClient();
            this.SmtpClient.Host        = Smtp.GetSection("Host").Value;
            this.SmtpClient.Port        = int.Parse(Smtp.GetSection("Port").Value);
            this.SmtpClient.Credentials = new NetworkCredential(Smtp.GetSection("User").Value, Smtp.GetSection("Password").Value);
            this.SmtpClient.EnableSsl   = bool.Parse(Smtp.GetSection("Ssl").Value);

            this.From = Smtp.GetSection("From").Value;
            this.Bcc = Smtp.GetSection("Bcc").Value.Split(";");
        }

        private MailMessage Message()
        {
            this.MailMessage = new MailMessage();
            this.MailMessage.From = new MailAddress(this.From);
            this.MailMessage.Subject = this.Subject;
            this.MailMessage.Body = this.Body;
            this.MailMessage.IsBodyHtml = true;

            this.MailTo();
            this.MailBcc();

            return MailMessage;
        }

        private void MailTo()
        {
            foreach (var Mail in this.To)
            {
                MailMessage.To.Add(Mail);
            }
        }

        private void MailBcc()
        {
            foreach (var Mail in this.Bcc)
            {
                MailMessage.Bcc.Add(Mail);
            }
        }

        public void Send()
        {
            try
            {
                SmtpClient.Send(this.Message());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
