namespace BuildingBlocks.Application.Behaviours;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger _logger;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger loggerManager)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        _logger = loggerManager ?? throw new ArgumentNullException(nameof(loggerManager));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var commandType = request.GetType().FullName;
        _logger.Information($"----- Validating command {commandType} --------");

        var errorList = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (errorList.Any())
        {
            var message = new
            {
                Message = "Validation errors",
                Command = commandType,
                Errors = errorList
            };

            _logger.Warning(message.Serialize());

            throw new ErrorListException(errorList.Select(_ => _.ErrorMessage));
        }

        return await next();
    }
}