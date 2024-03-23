namespace Users.Application.Handlers;
public record ResetPasswordParameters(string Token, string Email, string Password, string ConfirmPassword);

public sealed class ResetPasswordHandler : ICommandHandler<ResetPasswordCommand, Response>
{
    public record ResetPasswordCommand(string Token, string Email, string Password, string ConfirmPassword) : ICommand<Response>
    {
        public static ResetPasswordCommand CreateNew(ResetPasswordParameters parameters)
            => new(parameters.Token, parameters.Email, parameters.Password, parameters.ConfirmPassword);
    }

    private readonly IUserRepository _userRepository;

    public ResetPasswordHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Response> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException(StringMessages.UserNotFound);
        }

        var resetPassResult = await _userRepository.ResetUserPasswordAsync(user, request.Token, request.Password);

        if (resetPassResult.Succeeded)
        {
            await _userRepository.RemoveTokenAsync(user, TokenKeys.ResetPasswordLoginProvider, TokenKeys.ResetPasswordName);
            return Response.Ok();
        }
        else
        {
            var errors = resetPassResult.ReadErrors();
            throw new ErrorListException(errors);
        }
    }
}