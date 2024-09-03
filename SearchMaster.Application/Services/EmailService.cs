using Microsoft.Extensions.Options;
using MimeKit;
using SearchMaster.Infrastructure;

namespace SearchMaster.Application.Services
{
    public class EmailService(IOptions<EmailOptions> options) : IEmailService
    {
        private readonly EmailOptions _options = options.Value;

        public async Task SendEmail(string email, string subject, string message)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Search Master", _options.Login));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 465, true);
                await client.AuthenticateAsync(_options.Login, _options.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
