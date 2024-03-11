namespace Users.Application.Handlers;
public record UserInfoParameters();

public sealed class UserInfoHandler : ICommandHandler<UserInfoCommand, Response>
{
    public record UserInfoCommand : ICommand<Response>;

    public UserInfoHandler()
    {
    }

    public Task<Response> Handle(UserInfoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}