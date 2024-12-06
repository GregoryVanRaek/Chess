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
            Text = "Hello Chess-lover,\n\nWe’re excited to announce that a new tournament has been created! " +
                   "\n\nIf you’re ready to show your skills, head to our website to register and secure your spot before it’s too late!" +
                   "\n\nHere’s the link to register: localhost:4200\n\nDon’t miss out – we hope to see you in the game soon!" +
                   "\n\nBest regards,\nThe ChessTournament Team"
        };
        using var client = GetSmtpClient();
        client.Send(email);
        client.Disconnect(true);
    }

    public async Task SendCancellation(Member member)
    {
        MimeMessage email = new MimeMessage();
        email.From.Add(new MailboxAddress(_noReplyName, _noReplyEmail));
        email.To.Add(new MailboxAddress(member.Username, member.Mail)); 
        email.Subject = "New tournament";
        email.Body = new TextPart(TextFormat.Plain)
        {
            Text = "Hello Chess-lover,\n\nWe regret to inform you that a tournament has been canceled.\n" +
                   "\nIf you had planned to participate or were interested, we apologize for any inconvenience this may have caused." +
                   "\n\nStay tuned for upcoming tournaments and events on our website: localhost:4200." +
                   "\n\nThank you for your understanding, and we hope to see you in future games!" +
                   "\n\nBest regards,\nThe ChessTournament Team!"
                   
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