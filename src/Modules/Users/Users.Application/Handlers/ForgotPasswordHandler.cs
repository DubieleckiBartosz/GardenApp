namespace Users.Application.Handlers;
public record ForgotPasswordParameters(string Email);

public sealed class ForgotPasswordHandler : ICommandHandler<ForgotPasswordCommand, Response>
{
    public record ForgotPasswordCommand(string Email) : ICommand<Response>;
    private readonly IEmailClient _emailClient;

    public ForgotPasswordHandler(IEmailClient emailClient)
    {
        _emailClient = emailClient;
    }

    public Task<Response> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}