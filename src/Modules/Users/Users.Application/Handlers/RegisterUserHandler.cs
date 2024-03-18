namespace Users.Application.Handlers;

public record RegisterUserParameters(
    string City,
    string Email,
    string PhoneNumber,
    string FirstName,
    string LastName,
    string Password,
    string ConfirmPassword);

internal sealed class RegisterUserHandler : ICommandHandler<RegisterUserCommand, Response>
{
    private readonly IUsersEmailService _usersEmailService;
    private readonly IUserRepository _userRepository;
    private readonly UsersPathOptions _options;

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

    public RegisterUserHandler(
        IUsersEmailService usersEmailService,
        IUserRepository userRepository,
        IOptions<UsersPathOptions> options)
    {
        _usersEmailService = usersEmailService;
        _userRepository = userRepository;
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<Response> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userByEmail = await _userRepository.GetUserByEmailAsync(request.Email);
        if (userByEmail != null)
        {
            throw new AuthException(StringMessages.UnableNewAccount);
        }

        var user = User.NewUser(request.FirstName, request.LastName, request.City, request.PhoneNumber, request.Email);
        var createResult = await _userRepository.CreateUserAsync(user, request.Password);
        if (!createResult.Succeeded!)
        {
            var errors = createResult.ReadResult();
            return Response<IdentityErrorResponse>.Errors(errors);
        }

        var resultRole = await _userRepository.UserToRoleAsync(user, UserRole.User.ToString());
        if (!resultRole.Succeeded!)
        {
            var errors = resultRole.ReadResult();
            return Response<List<IdentityErrorResponse>>.Error(errors);
        }

        var token = await _userRepository.GenerateEmailConfirmationTokenAsync(user);

        var queryParams = new Dictionary<string, string>();

        queryParams["code"] = token;
        queryParams["email"] = user.Email;

        var verificationUri = QueryHelpers.AddQueryString(_options.RouteUri.ToString(), queryParams!);

        await _usersEmailService.SendEmailAsync(new() { user.Email },
            TemplateCreator.TemplateRegisterAccount(user.UserName, verificationUri), UserTemplateType.Confirmation);

        return Response.Ok(StringMessages.EmailSent);
    }
}