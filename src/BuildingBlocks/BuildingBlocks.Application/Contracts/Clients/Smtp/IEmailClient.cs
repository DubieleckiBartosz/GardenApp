using BuildingBlocks.Application.Models.Clients;

namespace BuildingBlocks.Application.Contracts.Clients.Smtp;

public interface IEmailClient
{
    Task SendEmailAsync(EmailDetails email);
}