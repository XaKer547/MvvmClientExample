using MimeKit;
using MvvmClientExample.Domain.Services;

namespace MvvmClientExample.Application.Services
{
    public class MailService : IMailService
    {
        private const string EMAIL = "projecttask7@rambler.ru";
        private const string PASSWORD = "XaKer_547";
        public async Task SendVerificationCode(int verificationCode, string userEmail)
        {
            using var message = new MimeMessage()
            {
                Subject = "Верификация",
                Body = new TextPart()
                {
                    Text = $"Ваш код верификации: {verificationCode}"
                }
            };

            message.From.Add(new MailboxAddress("SuperSusy", EMAIL));
            message.To.Add(new MailboxAddress("Alex", userEmail));

            using var client = new MailKit.Net.Smtp.SmtpClient();
            client.Timeout = 1000;

            await client.ConnectAsync("smtp.rambler.ru", 2525, false);
            await client.AuthenticateAsync(EMAIL, PASSWORD);

            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }
    }
}