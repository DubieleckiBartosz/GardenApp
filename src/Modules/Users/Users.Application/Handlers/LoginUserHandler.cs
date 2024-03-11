namespace Users.Application.Handlers;
public record LoginUserParameters(string Email, string Password);

public sealed class LoginUserHandler : ICommandHandler<LoginUserCommand, Response>
{
    public record LoginUserCommand : ICommand<Response>;

    public LoginUserHandler()
    {
    }

    public Task<Response> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}