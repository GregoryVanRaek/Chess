using ChessTournament.Domain.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace ChessTournament.API.Services;

public class MailService
{
    private readonly string _noReplyName;
    private readonly string _noReplyEmail;
    private readonly string _smtpHost;
    private readonly int _smtpPort;

    public MailService(IConfiguration configuration)
    {
        _noReplyName = configuration["Mail:Smtp:NoReply:Name"]!;
        _noReplyEmail = configuration.GetValue<string>("Mail:Smtp:NoReply:Email")!;
        _smtpHost = configuration.GetValue<string>("Mail:Smtp:Host")!;
        _smtpPort = configuration.GetValue<int>("Mail:Smtp:Port")!;

    }
    
    public void SendWelcome(Member member)
    {
        MimeMessage email = new MimeMessage();
        email.From.Add(new MailboxAddress(_noReplyName, _noReplyEmail));
        email.To.Add(new MailboxAddress(member.Username, member.Mail)); 
        email.Subject = "Welcome to ChessTournament";
        email.Body = new TextPart(TextFormat.Plain)
        {
            Text = "Welcome in our ChessTournament community! "
        };

        using var client = GetSmtpClient();
        client.Send(email);
        client.Disconnect(true);
    }

    public async Task SendInvitation(Member member)
    {
        MimeMessage email = new MimeMessage();
        email.From.Add(new MailboxAddress(_noReplyName, _noReplyEmail));
        email.To.Add(new MailboxAddress(member.Username, member.Mail)); 
        email.Subject = "New tournament";
        email.Body = new TextPart(TextFormat.Plain)
        {
            Text = "Hello Chess-lover ! A new tournament has been created. \nCome on our site to register asap if you want to participate !" +
                   "here is the link : localhost:4200\nWe hope you will join the game !"
        };
        using var client = GetSmtpClient();
        client.Send(email);
        client.Disconnect(true);
    }

    private SmtpClient GetSmtpClient()
    {
        SmtpClient client = new SmtpClient();
        client.Connect(_smtpHost, _smtpPort);
        
        return client;
    }
}