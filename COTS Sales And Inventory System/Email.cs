using System.IO;
using System.Net;
using System.Net.Mail;

namespace COTS_Sales_And_Inventory_System
{
    internal class Email
    {
        private readonly string _sender;
        private readonly string body;
        private readonly string fromPassword;
        private readonly string subject;

        public Email(string sender, string fromPassword, string subject, string body)
        {
            _sender = sender;
            this.fromPassword = fromPassword;
            this.subject = subject;
            this.body = body;
        }

        public void Send()
        {
            var fromAddress = new MailAddress(_sender, "Cots Sales Inveotry");
            var toAddress = new MailAddress(_sender, "Admin/Owner");
            
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
            })
            {
                smtp.Send(message);
            }
        }

    }
}