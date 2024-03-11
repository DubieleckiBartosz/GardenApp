using ILogger = Serilog.ILogger;

namespace GardenApp.API.Common;

public class ErrorHandlingMiddleware
{
    private const string ServerError = "Server Error";
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
    {
        this._next = next;
        this._logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this._next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.Error($"Handling error: {ex.Message}, InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        var response = CreateErrorResponse(exception, statusCode);

        var errorResponse = JsonConvert.SerializeObject(response);

        _logger.Error($"Error response: {errorResponse}");

        await httpContext.Response.WriteAsync(errorResponse);
    }

    private int GetStatusCode(Exception exception) =>
        exception switch
        {
            ArgumentOutOfRangeException => StatusCodes.Status400BadRequest,
            ArgumentNullException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            InvalidOperationException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            BaseException e => e.StatusCode == null ? StatusCodes.Status500InternalServerError : (int)e.StatusCode,
            _ => StatusCodes.Status500InternalServerError
        };

    private object CreateErrorResponse(Exception exception, int statusCode)
    {
        var title = string.Empty;

        if (exception is BaseException baseException)
        {
            title = baseException.Title;
        }

        var response = new
        {
            title = title ?? ServerError,
            status = statusCode,
            detail = ReadDetail(exception),
            errors = AssignErrors(exception)
        };

        return response;
    }

    private string ReadDetail(Exception ex)
    {
        //logic for external APIs
        return ex.Message;
    }

    private IReadOnlyList<string>? AssignErrors(Exception exception)
    {
        IReadOnlyList<string>? errors = null;

        if (exception is ValidationErrorListException validationErrorListException)
        {
            errors = validationErrorListException.Errors;
        }

        return errors;
    }
}

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}