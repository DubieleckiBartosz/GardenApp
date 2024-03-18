namespace Users.Application.Handlers;
public record ForgotPasswordParameters(string Email);

public sealed class ForgotPasswordHandler : ICommandHandler<ForgotPasswordCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IUsersEmailService _usersEmailService;
    private readonly UsersPathOptions _options;

    public record ForgotPasswordCommand(string Email) : ICommand<Response>;

    public ForgotPasswordHandler(
        IUserRepository userRepository,
        IUsersEmailService usersEmailService,
        IOptions<UsersPathOptions> options)
    {
        _userRepository = userRepository;
        _usersEmailService = usersEmailService;
        _options = options!.Value;
    }

    public async Task<Response> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email)
            ?? throw new AuthException(StringMessages.OperationFailed);

        var token = await _userRepository.GeneratePasswordResetTokenAsync(user);

        await _userRepository.SetTokenAsync(user, "ResetPassword", "ResetPasswordToken", token);

        await _usersEmailService.SendEmailAsync(new() { user.Email },
            TemplateCreator.TemplateResetPassword(token, _options.RouteUri.ToString()), UserTemplateType.ResetPassword);

        return Response.Ok();
    }
}