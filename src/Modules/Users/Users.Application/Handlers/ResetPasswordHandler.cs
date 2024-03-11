namespace Users.Application.Handlers;
public record ResetPasswordParameters(string Token, string Password, string ConfirmPassword);

public sealed class ResetPasswordHandler : ICommandHandler<ResetPasswordCommand, Response>
{
    public record ResetPasswordCommand(string Token, string Password, string ConfirmPassword) : ICommand<Response>
    {
        public static ResetPasswordCommand CreateNew(ResetPasswordParameters parameters)
            => new(parameters.Token, parameters.Password, parameters.ConfirmPassword);
    }

    public ResetPasswordHandler()
    {
    }

    public Task<Response> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}