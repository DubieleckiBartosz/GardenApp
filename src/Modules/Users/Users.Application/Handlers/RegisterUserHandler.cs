using BuildingBlocks.Application.Contracts.Clients.Smtp;
using Users.Application.Interfaces;

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
    private readonly IUserRepository _userRepository;

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

    public RegisterUserHandler(IEmailClient emailClient, IUserRepository userRepository)
    {
        _emailClient = emailClient;
        _userRepository = userRepository;
    }

    public async Task<Response> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.NewUser(request.FirstName, request.LastName, request.City, request.PhoneNumber, request.Email);
        var createResult = await _userRepository.CreateUserAsync(user, request.Password);
        return Response.Ok();
    }
}