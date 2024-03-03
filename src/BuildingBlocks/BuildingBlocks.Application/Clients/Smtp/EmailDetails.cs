namespace BuildingBlocks.Application.Clients.Smtp;

public class EmailDetails
{
    public List<string> Recipients { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public EmailDetails(List<string> recipients, string subject, string body)
    {
        Recipients = recipients;
        Subject = subject;
        Body = body;
    }
}