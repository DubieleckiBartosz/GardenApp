namespace Users.Application.Handlers;
public record ForgotPasswordParameters(string Email);

public sealed class ForgotPasswordHandler : ICommandHandler<ForgotPasswordCommand, Response>
{
    public record ForgotPasswordCommand(string Email) : ICommand<Response>;

    public ForgotPasswordHandler()
    {
    }

    public Task<Response> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}