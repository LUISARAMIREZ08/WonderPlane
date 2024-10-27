using MailKit.Net.Smtp;
using MimeKit;
using WonderPlane.Shared;
using MailKit.Security;
using MimeKit.Text;

namespace WonderPlane.Server.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailDto emailDto)
        {
            var email = new MimeMessage();
            string? user = _configuration.GetSection("Email:UserName").Value;
            string? password = _configuration.GetSection("Email:Password").Value;
            string? host = _configuration.GetSection("Email:Host").Value;
            string? portValue = _configuration.GetSection("Email:Port").Value;

            if (string.IsNullOrEmpty(host))
            {
                throw new InvalidOperationException("Host cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(portValue) || !int.TryParse(portValue, out int port))
            {
                throw new InvalidOperationException("Port must be a valid integer.");
            }

            // Enviar correo electronico
            email.From.Add(MailboxAddress.Parse(user));
            email.To.Add(MailboxAddress.Parse(emailDto.To));
            email.Subject = emailDto.Subject.ToString();
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = emailDto.Body.ToString()
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(user, password);
            await client.SendAsync(email);
            await client.DisconnectAsync(true);
        }
    }
}