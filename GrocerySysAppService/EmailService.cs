using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace GrocerySysAppService
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(string subject, string bodyText)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                _configuration["EmailSettings:FromName"],
                _configuration["EmailSettings:FromEmail"]
            ));
            message.To.Add(new MailboxAddress("Account Owner", "lorenzoraphaelgercayo6@gmail.com"));
            message.Subject = subject;
            message.Body = new TextPart("plain"){ Text = bodyText };

            using (var client = new SmtpClient())
            {
                string host = _configuration["EmailSettings:SmtpHost"];
                int port = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? "2525");

                if (string.IsNullOrEmpty(host))
                {
                    throw new InvalidOperationException("EmailSettings:SmtpHost is null or empty. Ensure appsettings.json is copied to the output directory.");
                }

                client.Connect(
                    host,
                    port,
                    SecureSocketOptions.StartTls
                );

                client.Authenticate(
                    _configuration["EmailSettings:Username"],
                    _configuration["EmailSettings:Password"]
                );

                client.Send(message);
                client.Disconnect(true);
            }
        }

        public void SendAccountNotification(string username, string action)
        {
            string subject = $"Account Alert: {action}";
            string body = $"Hello {username},\n\n your account has been: {action} in the system.\n\nIf you did not initiate this change, please contact support immediately.";

            SendEmail(subject, body);
        }

        public void SendItemNotification(string itemId, string itemName, string action)
        {
            string subject = $"Inventory Alert: Item {itemId}";
            string body = $"Item (ID: {itemId}), {itemName} {action} in the inventory system.";

            SendEmail(subject, body);
        }
        
    }
}