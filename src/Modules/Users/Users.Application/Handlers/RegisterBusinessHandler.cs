namespace Users.Application.Handlers;

public record RegisterBusinessParameters(
    string Email,
    string Name,
    string PhoneNumber,
    string Password,
    string ConfirmPassword);

public sealed class RegisterBusinessHandler : ICommandHandler<RegisterBusinessCommand, Response>
{
    private readonly IUsersEmailService _usersEmailService;
    private readonly IUserRepository _userRepository;
    private readonly IPanelClient _panelClient;
    private readonly UsersPathOptions _options;
    public record RegisterBusinessCommand(
        string Email,
        string Name,
        string PhoneNumber,
        string Password,
        string ConfirmPassword) : ICommand<Response>
    {
        public static RegisterBusinessCommand NewCommand(RegisterBusinessParameters parameters)
        {
            return new RegisterBusinessCommand(
                parameters.Email,
                parameters.Name,
                parameters.PhoneNumber,
                parameters.Password,
                parameters.ConfirmPassword);
        }
    }

    public RegisterBusinessHandler(
        IUsersEmailService usersEmailService,
        IUserRepository userRepository,
        IOptions<UsersPathOptions> options,
        IPanelClient panelClient)
    {
        _usersEmailService = usersEmailService;
        _userRepository = userRepository;
        _panelClient = panelClient;
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<Response> Handle(RegisterBusinessCommand request, CancellationToken cancellationToken)
    {
        var userByEmail = await _userRepository.GetUserByEmailAsync(request.Email);
        if (userByEmail != null)
        {
            throw new AuthException(StringMessages.UnableNewAccount);
        }

        var businessUser = User.NewBusinessUser(request.Name, request.PhoneNumber, request.Email);

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var createResult = await _userRepository.CreateUserAsync(businessUser, request.Password);
            if (!createResult.Succeeded!)
            {
                var errors = createResult.ReadErrors();
                throw new ErrorListException(errors);
            }

            var resultRole = await _userRepository.UserToRoleAsync(businessUser, UserRole.Business);
            if (!resultRole.Succeeded!)
            {
                var errors = createResult.ReadErrors();
                throw new ErrorListException(errors);
            }
            var resultClient = await _panelClient.CreateNewContractorAsync(
                new CreateNewContractorRequest(businessUser.Email, request.Name, businessUser.Id, request.PhoneNumber));

            if (resultClient!.Success)
            {
                scope.Complete();
            }
        }

        var token = await _userRepository.GenerateEmailConfirmationTokenAsync(businessUser);

        var queryParams = new Dictionary<string, string>();

        queryParams["code"] = token;
        queryParams["email"] = businessUser.Email;

        var verificationUri = QueryHelpers.AddQueryString(_options.ConfirmRouteUri.ToString(), queryParams!);

        await _usersEmailService.SendEmailAsync(new() { businessUser.Email },
            TemplateCreator.TemplateRegisterAccount(businessUser.UserName, verificationUri), UserTemplateType.Confirmation);

        return Response.Ok(StringMessages.EmailSent);
    }
}