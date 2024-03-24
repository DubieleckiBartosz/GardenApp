namespace Users.Application.Handlers;

public sealed class ConfirmUserHandler : ICommandHandler<ConfirmUserCommand, Response>
{
    public record ConfirmUserCommand(string Code, string Email) : ICommand<Response>;
    private readonly IUserRepository _userRepository;
    private readonly IPanelClient _panelClient;

    public ConfirmUserHandler(IUserRepository userRepository, IPanelClient panelClient)
    {
        _userRepository = userRepository;
        _panelClient = panelClient;
    }

    public async Task<Response> Handle(ConfirmUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException(StringMessages.UserNotFound);
        }

        var result = await _userRepository.ConfirmUserAsync(user!, request.Code);
        if (!result.Succeeded!)
        {
            var errors = result.ReadErrors();
            throw new ErrorListException(errors);
        }

        var resultClient = await _panelClient.CreateNewContractorAsync(
           new CreateNewContractorRequest(user.Email, user.UserName, user.Id, user.PhoneNumber));

        if (!resultClient!.Success)
        {
            if (resultClient.Errors != null)
            {
                throw new ErrorListException(resultClient.Errors);
            }

            throw new InvalidOperationException(StringMessages.OperationFailed);
        }

        return Response.Ok();
    }
}