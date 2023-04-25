using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using TourneyRent.Contracts.Options;

namespace TourneyRent.BusinessLogic.Services;

public class MailService
{
    private readonly MailOptions _mailOptions;

    public MailService(IOptions<MailOptions> mailOptions)
    {
        _mailOptions = mailOptions.Value;
    }

    public async Task SendEmailAsync(string subject, string name, string email, string message)
    {
        if (!_mailOptions.UseSmtp4Dev)
        {
            return;
        }

        var mimeMessage = new MimeMessage();

        mimeMessage.From.Add(new MailboxAddress(_mailOptions.SenderName, _mailOptions.SenderEmail));
        mimeMessage.To.Add(new MailboxAddress(name, email));
        mimeMessage.Subject = subject;

        mimeMessage.Body = new TextPart("plain")
        {
            Text = message
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(_mailOptions.Host, _mailOptions.Port, SecureSocketOptions.None);

        client.Capabilities &= ~SmtpCapabilities.Pipelining;

        await client.SendAsync(mimeMessage);
        await client.DisconnectAsync(true);
    }
}
