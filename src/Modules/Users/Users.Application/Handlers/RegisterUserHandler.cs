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

    public RegisterUserHandler()
    {
    }

    public Task<Response> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}