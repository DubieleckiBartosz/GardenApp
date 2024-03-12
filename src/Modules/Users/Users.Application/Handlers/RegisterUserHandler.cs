using BuildingBlocks.Application.Contracts.Clients.Smtp;
using Users.Domain.Users;

namespace Users.Application.Handlers;

public record RegisterUserParameters(
    string City,
    string Email,
    string PhoneNumber,
    string FirstName,
    string LastName,
    string Password,
    string ConfirmPassword);

public sealed class RegisterUserHandler : ICommandHandler<RegisterUserCommand, Response>
{
    private readonly IEmailClient _emailClient;

    public record RegisterUserCommand(
        string City,
        string Email,
        string PhoneNumber,
        string FirstName,
        string LastName,
        string Password,
        string ConfirmPassword) : ICommand<Response>
    {
        public static RegisterUserCommand NewCommand(RegisterUserParameters parameters)
        {
            return new RegisterUserCommand(
                parameters.City,
                parameters.Email,
                parameters.PhoneNumber,
                parameters.FirstName,
                parameters.LastName,
                parameters.Password,
                parameters.ConfirmPassword);
        }
    }

    public RegisterUserHandler(IEmailClient emailClient)
    {
        _emailClient = emailClient;
    }

    public async Task<Response> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.NewUser(request.FirstName, request.LastName, request.City, request.PhoneNumber, request.Email);

        return Response.Ok();
    }
}